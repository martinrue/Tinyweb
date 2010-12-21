using System.IO;
using System.Text;
using NHaml;

namespace tinyweb.framework
{
    public static class NHamlCompiler
    {
        public static string Compile<T>(T model, params string[] templates)
        {
            var engine = new TemplateEngine();
            var renderedView = new StringBuilder();
            var writer = new StringWriter(renderedView);

            var template = engine.Compile(templates, typeof(NHamlView<T>));

            var view = template.CreateInstance() as NHamlView<T>;
            
            view.Model = model;
            view.Render(writer);
            writer.Flush();

            return renderedView.ToString();
        }
    }
}