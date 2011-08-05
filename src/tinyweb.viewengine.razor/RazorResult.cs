using System;
using System.IO;
using tinyweb.framework;

namespace tinyweb.viewengine.razor
{
    public class RazorResult<T> : IResult
    {
        public T Model { get; set; }
        public string ViewPath { get; set; }
        public string MasterPath { get; set; }

        public RazorResult(T model, string templateName, string master = null)
        {
            Model = model;
            ViewPath = templateName;
            MasterPath = master;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            var fullTemplatePath = Helpers.ResolveTemplatePath(ViewPath);

            if (fullTemplatePath == null)
            {
                throw new FileNotFoundException(String.Format("The razor view at {0} could not be found", ViewPath));
            }

            response.ContentType = "text/html";
            response.Write(RazorCompiler.Render(Model, fullTemplatePath, MasterPath));
        }
    }

    public class RazorResult : IResult
    {
        public string ViewPath { get; set; }
        public string MasterPath { get; set; }

        public RazorResult(string templateName, string master = null)
        {
            ViewPath = templateName;
            MasterPath = master;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            var fullTemplatePath = Helpers.ResolveTemplatePath(ViewPath);

            if (fullTemplatePath == null)
            {
                throw new FileNotFoundException(String.Format("The razor view at {0} could not be found", ViewPath));
            }

            response.ContentType = "text/html";
            response.Write(RazorCompiler.Render(fullTemplatePath, MasterPath));
        }
    }
}