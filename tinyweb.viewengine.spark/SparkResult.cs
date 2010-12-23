using System;
using System.Collections.Generic;
using System.IO;
using tinyweb.framework;

namespace tinyweb.viewengine.spark
{
    public class SparkResult<T> : IHandlerResult
    {
        T model;
        string templatePath;
        string templateName;
        string master;

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

        public SparkResult(T model, string templatesPath, string master = null)
        {           
            var templateName = Path.GetFileName(templatesPath);
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(templatesPath));

            var fullTemplatePath = Path.Combine(templatePath, templateName);

            if (!File.Exists(fullTemplatePath))
            {
                throw new FileNotFoundException(String.Format("The spark view at {0} could not be found", fullTemplatePath));
            }

            this.model = model;
            this.templatePath = templatePath;
            this.templateName = templateName;
            this.master = master;
        }

        public string GetResult()
        {
            return SparkCompiler.Compile(model, templatePath, templateName, master);
        }
    }

    public class SparkResult : IHandlerResult
    {
        string templatePath;
        string templateName;
        string master;

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

        public SparkResult(string templatesPath, string master = null)
        {
            var templateName = Path.GetFileName(templatesPath);
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(templatesPath));

            var fullTemplatePath = Path.Combine(templatePath, templateName);

            if (!File.Exists(fullTemplatePath))
            {
                throw new FileNotFoundException(String.Format("The spark view at {0} could not be found", fullTemplatePath));
            }

            this.templatePath = templatePath;
            this.templateName = templateName;
            this.master = master;
        }

        public static implicit operator SparkResult(string templateName)
        {
            return new SparkResult(templateName);
        }

        public string GetResult()
        {
            return SparkCompiler.Compile<Object>(null, templatePath, templateName, master);
        }
    }
}