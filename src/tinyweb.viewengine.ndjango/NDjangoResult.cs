using System;
using System.IO;
using tinyweb.framework;

namespace tinyweb.viewengine.ndjango
{
    public class NDjangoResult<T> : IHandlerResult
    {
        T _model;
        string _template;

        public NDjangoResult(T model, string template)
        {  
            var fullTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, template);

            if (!File.Exists(fullTemplatePath))
            {
                throw new FileNotFoundException(String.Format("The django view at {0} could not be found", fullTemplatePath));
            }

            _template = fullTemplatePath;
            _model = model;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Write(NDjangoCompiler.Compile(_model, _template));
        }
    }

    public class NDjangoResult : IHandlerResult
    {
        string _template;

        public NDjangoResult(string template)
        {
            var fullTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, template);

            if (!File.Exists(fullTemplatePath))
            {
                throw new FileNotFoundException(String.Format("The django view at {0} could not be found", fullTemplatePath));
            }

            _template = fullTemplatePath;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Write(NDjangoCompiler.Compile<Object>(null, _template));
        }
    }
}