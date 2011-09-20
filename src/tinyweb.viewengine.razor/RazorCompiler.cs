using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Razor;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;
using Microsoft.CSharp;

namespace tinyweb.viewengine.razor
{
    public class RazorCompiler
    {
        //cache of already compiled types
        static ConcurrentDictionary<Tuple<string, Type>, Type> cache = new ConcurrentDictionary<Tuple<string, Type>, Type>();

        public static string Render(string path, string masterPath = null)
        {
            return Render(new object(), path, masterPath);
        }

        public static string Render<T>(T model, string templatePath, string masterName = null)
        {
            var instance = GetCompiledTemplate<T>(model, templatePath);

            string result = "";

            instance.Model = model;
            instance.Execute();
            instance.Layout = masterName ?? instance.Layout;

            if (instance.Layout != null)
            {
                result = RenderMasterView<T>(model, templatePath, instance);
            }
            else
            {
                result = instance.Result;
            }

            return result;
        }

        //gets the pre-compiled template - or else compiles it
        private static TemplateBase<T> GetCompiledTemplate<T>(T model, string path)
        {
            var key = Tuple.Create(path, typeof(T));
            Type type;

            if (!cache.TryGetValue(key, out type))
            {
                type = GetCompiledType<T>(File.ReadAllText(path));

                if (CachingIsEnabled())
                {
                    cache[key] = type;
                }
            }

            var instance = (TemplateBase<T>)Activator.CreateInstance(type);
            instance.Path = path;
            return instance;
        }

        private static bool CachingIsEnabled()
        {
            return HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled;
        }

        //used to render the master
        private static string RenderMasterView<T>(T model, string templatePath, TemplateBase<T> instance)
        {
            var masterPath = Helpers.ResolveTemplatePath(instance.Layout, new[] { Path.GetDirectoryName(templatePath) });

            var masterInstance = GetCompiledTemplate<object>(model, masterPath);
            //RenderBody is a func that we can overwrite
            masterInstance.RenderBody = () =>
            {
                return instance.Result;
            };

            masterInstance.Execute();

            return masterInstance.Result;
        }

        private static Type GetCompiledType<T>(string template)
        {
            var key = "c" + Guid.NewGuid().ToString("N");

            var parser = new HtmlMarkupParser();

            var baseType = typeof(TemplateBase<>).MakeGenericType(typeof(T));

            var regex = new Regex("@model.*");
            template = regex.Replace(template, "");

            var host = new RazorEngineHost(new CSharpRazorCodeLanguage(), () => parser)
            {
                DefaultBaseClass = baseType.FullName,
                DefaultClassName = key,
                DefaultNamespace = "tinyweb.viewengine.razor.dynamic",
                GeneratedClassContext = new GeneratedClassContext("Execute", "Write", "WriteLiteral", "WriteTo", "WriteLiteralTo", "tinyweb.viewengine.razor.RazorCompiler.TemplateBase")
            };

            //always include this one
            host.NamespaceImports.Add("tinyweb.framework.Helpers");
            host.NamespaceImports.Add("tinyweb.viewengine.razor");
            host.NamespaceImports.Add("System");

            //read web.config pages/namespaces
            if (File.Exists("\\web.config"))
            {
                var config = WebConfigurationManager.OpenWebConfiguration("\\web.config");
                var pages = config.GetSection("system.web/pages");
                if (pages != null)
                {
                    PagesSection pageSection = (PagesSection)pages;
                    for (int i = 0; i < pageSection.Namespaces.Count; i++)
                    {
                        //this automatically ignores namespaces already added
                        host.NamespaceImports.Add(pageSection.Namespaces[i].Namespace);
                    }
                }
            }

            CodeCompileUnit code;
            using (var reader = new StringReader(template))
            {
                var generatedCode = new RazorTemplateEngine(host).GenerateCode(reader);
                code = generatedCode.GeneratedCode;
            }

            var @params = new CompilerParameters
            {
                IncludeDebugInformation = false,
                TempFiles = new TempFileCollection(AppDomain.CurrentDomain.DynamicDirectory),
                CompilerOptions = "/target:library /optimize",
                GenerateInMemory = false
            };

            var assemblies = AppDomain.CurrentDomain
               .GetAssemblies()
               .Where(a => !a.IsDynamic)
               .Select(a => a.Location)
               .ToArray();

            @params.ReferencedAssemblies.AddRange(assemblies);

            var provider = new CSharpCodeProvider();
            var compiled = provider.CompileAssemblyFromDom(@params, code);

            if (compiled.Errors.Count > 0)
            {
                var compileErrors = string.Join("\r\n", compiled.Errors.Cast<object>().Select(o => o.ToString()));
                throw new ApplicationException("Failed to compile Razor:" + compileErrors);
            }

            return compiled.CompiledAssembly.GetType("tinyweb.viewengine.razor.dynamic." + key);
        }
    }
}