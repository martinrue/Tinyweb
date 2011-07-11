using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tinyweb.framework;
using System.IO;

namespace tinyweb.viewengine.razor {
    public class RazorResult<T> : RazorResult, IResult {
        T _model;

        public RazorResult(T model, string templateName, string master = null)
            : base(templateName, master) {
            _model = model;
        }

        public override void ProcessResult(IRequestContext request, IResponseContext response) {
            response.ContentType = "text/html";
            response.Write(RazorCompiler.Render(_model, _templateName, _master));
        }
    }

    public class RazorResult : IResult {
        internal string _templateName;
        internal string _master;

        public RazorResult(string templateName, string master = null) {
            var templatePath = AppDomain.CurrentDomain.BaseDirectory;
            string fullTemplatePath = "";
            fullTemplatePath = Helpers.ResolveTemplatePath(templateName);
            if (fullTemplatePath == null) {
                throw new FileNotFoundException(String.Format("The razor view at {0} could not be found", templateName));
            }

            _templateName = fullTemplatePath;
            _master = master;
        }

        public virtual void ProcessResult(IRequestContext request, IResponseContext response) {
            response.ContentType = "text/html";
            response.Write(RazorCompiler.Render(_templateName));
        }
    }
}
