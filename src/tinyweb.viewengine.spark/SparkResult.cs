using System;
using System.IO;
using tinyweb.framework;

namespace tinyweb.viewengine.spark
{
    public class SparkResult<T> : IResult
    {
        public T Model { get; set; }
        public string ViewPath { get; set; }
        public string MasterPath { get; set; }

        string _templatePath;
        string _templateName;
        
        public SparkResult(T model, string templatesPath, string master = null)
        {
            Model = model;
            ViewPath = templatesPath;
            MasterPath = master;

            _templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(templatesPath));
            _templateName = Path.GetFileName(templatesPath);
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            var fullTemplatePath = Path.Combine(_templatePath, _templateName);

            if (!File.Exists(fullTemplatePath))
            {
                throw new FileNotFoundException(String.Format("The spark view at {0} could not be found", fullTemplatePath));
            }

            response.ContentType = "text/html";
            response.Write(SparkCompiler.Compile(Model, _templatePath, _templateName, MasterPath));
        }
    }

    public class SparkResult : IResult
    {
        public string ViewPath { get; set; }
        public string MasterPath { get; set; }

        string _templatePath;
        string _templateName;

        public SparkResult(string templatesPath, string master = null)
        {
            ViewPath = templatesPath;
            MasterPath = master;

            _templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(templatesPath));
            _templateName = Path.GetFileName(templatesPath);
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            var fullTemplatePath = Path.Combine(_templatePath, _templateName);

            if (!File.Exists(fullTemplatePath))
            {
                throw new FileNotFoundException(String.Format("The spark view at {0} could not be found", fullTemplatePath));
            }

            response.ContentType = "text/html";
            response.Write(SparkCompiler.Compile<Object>(null, _templatePath, _templateName, MasterPath));
        }
    }
}