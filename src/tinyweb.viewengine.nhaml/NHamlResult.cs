using System;
using System.IO;
using System.Linq;
using tinyweb.framework;

namespace tinyweb.viewengine.nhaml
{
    public class NHamlResult<T> : IHandlerResult
    {
        T _model;
        string[] _templates;

        public NHamlResult(T model, params string[] templates)
        {  
            var fullTemplates = templates.Select(template =>
            {
                var fullTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, template);

                if (!File.Exists(fullTemplatePath))
                {
                    throw new FileNotFoundException(String.Format("The haml view at {0} could not be found", fullTemplatePath));
                }

                return fullTemplatePath;

            }).ToArray();

            _templates = fullTemplates;
            _model = model;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Write(NHamlCompiler.Compile(_model, _templates));
        }
    }

    public class NHamlResult : IHandlerResult
    {
        string[] _templates;

        public NHamlResult(params string[] templates)
        {
            var fullTemplates = templates.Select(template =>
            {
                var fullTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, template);

                if (!File.Exists(fullTemplatePath))
                {
                    throw new FileNotFoundException(String.Format("The haml view at {0} could not be found", fullTemplatePath));
                }

                return fullTemplatePath;

            }).ToArray();

            _templates = fullTemplates;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Write(NHamlCompiler.Compile<Object>(null, _templates));
        }
    }
}