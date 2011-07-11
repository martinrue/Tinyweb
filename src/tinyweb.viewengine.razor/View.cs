using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tinyweb.viewengine.razor {
    public static class View {
        public static RazorResult<T> Razor<T>(T model, string templatePath, string master = null) {
            return new RazorResult<T>(model, templatePath, master);
        }

        public static RazorResult Razor(string templatePath, string master = null) {
            return new RazorResult(templatePath, master);
        }
    }
}
