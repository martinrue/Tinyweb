using System;
using System.Collections.Generic;
using System.IO;
using tinyweb.framework;

namespace tinyweb.viewengine.ndjango
{
    public class NDjangoResult<T> : IHandlerResult
    {
        T model;
        string template;

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

        public NDjangoResult(T model, string template)
        {  
            var fullTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, template);

            if (!File.Exists(fullTemplatePath))
            {
                throw new FileNotFoundException(String.Format("The django view at {0} could not be found", fullTemplatePath));
            }

            this.template = fullTemplatePath;
            this.model = model;
        }

        public string GetResult()
        {
            return NDjangoCompiler.Compile(model, template);
        }
    }

    public class NDjangoResult : IHandlerResult
    {
        string template;

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

        public NDjangoResult(string template)
        {
            var fullTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, template);

            if (!File.Exists(fullTemplatePath))
            {
                throw new FileNotFoundException(String.Format("The django view at {0} could not be found", fullTemplatePath));
            }

            this.template = fullTemplatePath;
        }

        public string GetResult()
        {
            return NDjangoCompiler.Compile<Object>(null, template);
        }
    }
}