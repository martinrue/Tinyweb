using System;
using System.IO;
using tinyweb.framework;

namespace tinyweb.viewengine.spark
{
    public class SparkResult<T> : IHandlerResult
    {
        T _model;
        string _templatePath;
        string _templateName;
        string _master;
        
        public SparkResult(T model, string templatesPath, string master = null)
        {           
            var templateName = Path.GetFileName(templatesPath);
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(templatesPath));

            var fullTemplatePath = Path.Combine(templatePath, templateName);

            if (!File.Exists(fullTemplatePath))
            {
                throw new FileNotFoundException(String.Format("The spark view at {0} could not be found", fullTemplatePath));
            }

            _model = model;
            _templatePath = templatePath;
            _templateName = templateName;
            _master = master;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Write(SparkCompiler.Compile(_model, _templatePath, _templateName, _master));
        }
    }

    public class SparkResult : IHandlerResult
    {
        string _templatePath;
        string _templateName;
        string _master;

        public SparkResult(string templatesPath, string master = null)
        {
            var templateName = Path.GetFileName(templatesPath);
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(templatesPath));

            var fullTemplatePath = Path.Combine(templatePath, templateName);

            if (!File.Exists(fullTemplatePath))
            {
                throw new FileNotFoundException(String.Format("The spark view at {0} could not be found", fullTemplatePath));
            }

            _templatePath = templatePath;
            _templateName = templateName;
            _master = master;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Write(SparkCompiler.Compile<Object>(null, _templatePath, _templateName, _master));
        }
    }
}