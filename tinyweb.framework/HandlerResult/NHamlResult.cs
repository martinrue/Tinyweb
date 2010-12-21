using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tinyweb.framework
{
    public class NHamlResult<T> : IHandlerResult
    {
        T model;
        string[] templates;

        public HandlerResultType ResultType
        {
            get { return HandlerResultType.Render; }
        }

        public IDictionary<string, string> CustomHeaders
        {
            get { return new Dictionary<string, string>(); }
        }

        public string ContentType
        {
            get { return "text/html"; }
        }

        public NHamlResult(T model, params string[] templates)
        {  
            var fullTemplates = templates.Select(t =>
            {
                var fullTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, t);

                if (!File.Exists(fullTemplatePath))
                {
                    throw new FileNotFoundException(String.Format("The spark view at {0} could not be found", fullTemplatePath));
                }

                return fullTemplatePath;

            }).ToArray();

            this.templates = fullTemplates;
            this.model = model;
        }

        public string GetResult()
        {
            return NHamlCompiler.Compile(model, templates);
        }
    }

    public class NHamlResult : IHandlerResult
    {
        string[] templates;

        public HandlerResultType ResultType
        {
            get { return HandlerResultType.Render; }
        }

        public IDictionary<string, string> CustomHeaders
        {
            get { return new Dictionary<string, string>(); }
        }

        public string ContentType
        {
            get { return "text/html"; }
        }

        public NHamlResult(params string[] templates)
        {
            var fullTemplates = templates.Select(t =>
            {
                var fullTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, t);

                if (!File.Exists(fullTemplatePath))
                {
                    throw new FileNotFoundException(String.Format("The spark view at {0} could not be found", fullTemplatePath));
                }

                return fullTemplatePath;

            }).ToArray();

            this.templates = fullTemplates;
        }

        public string GetResult()
        {
            return NHamlCompiler.Compile<Object>(null, templates);
        }
    }
}