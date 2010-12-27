using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using NDjango;
using NDjango.Interfaces;

namespace tinyweb.viewengine.ndjango
{
    public static class NDjangoCompiler
    {
        public static string Compile<T>(T model, string template)
        {
            var templateManagerProvider = new TemplateManagerProvider().WithLoader(new TemplateLoader());
            var manager = templateManagerProvider.GetNewManager();
            
            var context = new Dictionary<string, object> { { "Model", model } };
            var reader = manager.RenderTemplate(template, context);

            var text = reader.ReadToEnd();
            reader.Close();

            return text;
        }
    }

    public class TemplateLoader : ITemplateLoader
    {
        public TextReader GetTemplate(string template)
        {
            return File.Exists(template) ? new StreamReader(template) : new StreamReader(HttpContext.Current.Server.MapPath(template));
        }

        public bool IsUpdated(string template, DateTime timestamp)
        {
            return File.GetLastWriteTime(template) > timestamp;
        }
    }
}