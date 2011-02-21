using System.IO;
using System.Text;
using NHaml;

namespace tinyweb.viewengine.nhaml
{
    public static class NHamlCompiler
    {
        public static string Compile<T>(T model, params string[] templates)
        {
            var engine = new TemplateEngine();
            var template = engine.Compile(templates, typeof(NHamlView<T>));
            var view = template.CreateInstance() as NHamlView<T>;

            var buffer = new StringBuilder();
            var writer = new StringWriter(buffer);

            view.Model = model;
            view.Render(writer);
            writer.Flush();

            return buffer.ToString();
        }
    }
}