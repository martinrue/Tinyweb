using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.IO;
using System.Web;
using Spark;
using Spark.FileSystem;

namespace tinyweb.viewengine.spark
{
    public static class SparkCompiler
    {
        static ConcurrentDictionary<string, ISparkViewEntry> cache = new ConcurrentDictionary<string, ISparkViewEntry>();

        public static string Compile<T>(T model, string templatesPath, string templateName, string master)
        {
            var fullTemplatePath = Path.Combine(templatesPath, templateName);
            var templateFilename = Path.GetFileName(fullTemplatePath);

            var sparkEngine = buildSparkEngine(templatesPath);
            var descriptor = buildDescriptor(templateFilename, master);
            var viewEntry = buildSparkViewEntry(fullTemplatePath, sparkEngine, master, descriptor);
            var view = viewEntry.CreateInstance();

            if (model != null)
            {
                (view as SparkView<T>).Model = model;
            }

            var output = new StringWriter();
            view.RenderView(output);
            sparkEngine.ReleaseInstance(view);

            return output.ToString();
        }

        private static SparkViewEngine buildSparkEngine(string templatesPath)
        {
            var sparkEngine = new SparkViewEngine
            {
                ViewFolder = new FileSystemViewFolder(templatesPath),
                DefaultPageBaseType = typeof(SparkView).FullName
            };

            if (ConfigurationManager.GetSection("spark") == null)
            {
                sparkEngine.Settings = new SparkSettings()
                    .AddNamespace("System")
                    .AddNamespace("System.Collections.Generic")
                    .AddNamespace("System.Linq")
                    .AddNamespace("tinyweb.framework")
                    .AddNamespace("tinyweb.framework.Helpers");
            }

            return sparkEngine;
        }

        private static SparkViewDescriptor buildDescriptor(string templateFilename, string master)
        {
            var descriptor = new SparkViewDescriptor().AddTemplate(templateFilename);

            if (!String.IsNullOrEmpty(master))
            {
                descriptor.AddTemplate(master);
            }

            return descriptor;
        }

        private static ISparkViewEntry buildSparkViewEntry(string fullTemplatePath, SparkViewEngine sparkEngine, string master, SparkViewDescriptor descriptor)
        {
            ISparkViewEntry entry;
            var key = fullTemplatePath + master;

            if (!cache.ContainsKey(key))
            {
                entry = sparkEngine.CreateEntry(descriptor);

                if (cachingIsEnabled())
                {
                    cache[key] = entry;
                }
            }
            else
            {
                entry = cache[key];
            }

            return entry;
        }

        private static bool cachingIsEnabled()
        {
            return HttpContext.Current != null && !HttpContext.Current.IsDebuggingEnabled;
        }
    }
}