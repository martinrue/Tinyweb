using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace tinyweb.viewengine.razor {
    public class HtmlHelper<T> {

        TemplateBase<T> template;

        public string Path {
            get {
                return template.Path;
            }
        }

        public HtmlHelper(TemplateBase<T> templateBase) {
            this.template = templateBase;
        }
    }

    public static class Helpers {
        static string[] ViewPaths = new string[] {
            "Views/{0}.cshtml",
            "{0}.cshtml",
            "Views/Shared/{0}.cshtml",
            "Shared/{0}.cshtml",
            //These are for template names that include extension
            "Views/{0}",
            "{0}",
            "Views/Shared/{0}",
            "Shared/{0}",
        };


        public static string Partial<T>(this HtmlHelper<T> helper, string partialName, object model = null) {
            string fullTemplatePath = Helpers.ResolveTemplatePath(partialName, new string[] { System.IO.Path.GetDirectoryName(helper.Path) });

            return RazorCompiler.Render(model, fullTemplatePath, null);
        }

        //resolves the template name to a file by searching ViewPaths
        public static string ResolveTemplatePath(string templateName, string[] optionalSearchPaths = null) {
            var templatePath = AppDomain.CurrentDomain.BaseDirectory;

            string fullTemplatePath = null;

            foreach (var path in ViewPaths) {
                string fullPath = System.IO.Path.Combine(templatePath, string.Format(path, templateName));
                if (System.IO.File.Exists(fullPath)) {
                    fullTemplatePath = fullPath;
                    break;
                }
            }

            if (fullTemplatePath == null && optionalSearchPaths != null)
                foreach (var optionalPath in optionalSearchPaths) {
                    foreach (var path in ViewPaths) {
                        string fullPath = System.IO.Path.Combine(optionalPath, string.Format(path, templateName));
                        if (System.IO.File.Exists(fullPath)) {
                            fullTemplatePath = fullPath;
                            break;
                        }
                    }
                }

            return fullTemplatePath;
        }
    }
}
