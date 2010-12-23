using System;
using System.IO;
using Spark;
using Spark.FileSystem;

namespace tinyweb.viewengine.spark
{
    public static class SparkCompiler
    {
        public static string Compile<T>(T model, string templatesPath, string templateName, string master)
        {
            var fullTemplatePath = Path.Combine(templatesPath, templateName);
            var templateFilename = Path.GetFileName(fullTemplatePath);

            var viewFolder = new FileSystemViewFolder(templatesPath);
            
            var settings = new SparkSettings()
                .AddNamespace("System")
                .AddNamespace("System.Collections.Generic")
                .AddNamespace("System.Linq");

            var sparkEngine = new SparkViewEngine
            {
                ViewFolder = viewFolder,
                Settings = settings
            };

            if (model != null)
            {
                sparkEngine.DefaultPageBaseType = "tinyweb.viewengine.spark.SparkView";
            }

            var descriptor = new SparkViewDescriptor().AddTemplate(templateFilename);

            if (!String.IsNullOrEmpty(master))
            {
                descriptor.AddTemplate(master);
            }

            var view = sparkEngine.CreateInstance(descriptor);

            if (model != null)
            {
                (view as SparkView<T>).Model = model;
            }

            var output = new StringWriter();
            view.RenderView(output);

            sparkEngine.ReleaseInstance(view);
            
            return output.ToString();
        }
    }
}