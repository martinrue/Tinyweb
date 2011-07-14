using System;
using System.IO;
using System.Text;

namespace tinyweb.viewengine.razor
{
    public abstract class TemplateBase<T>
    {
        public string Layout { get; set; }

        public Func<string> RenderBody { get; set; }

        public T Model { get; set; }

        protected TemplateBase()
        {
            Builder = new StringBuilder();
            Html = new HtmlHelper<T>(this);
        }

        public HtmlHelper<T> Html { get; set; }
        public string Path { get; internal set; }
        public StringBuilder Builder { get; private set; }
        public string Result { get { return Builder.ToString(); } }

        public void Clear()
        {
            Builder.Clear();
        }

        public virtual void Execute() { }

        public void Write(object @object)
        {
            if (@object == null)
            {
                return;
            }

            Builder.Append(@object);
        }

        public void WriteLiteral(string @string)
        {
            if (@string == null)
            {
                return;
            }

            Builder.Append(@string);
        }

        public static void WriteLiteralTo(TextWriter writer, string literal)
        {
            if (literal == null)
            {
                return;
            }

            writer.Write(literal);
        }


        public static void WriteTo(TextWriter writer, object obj)
        {
            if (obj == null)
            {
                return;
            }

            writer.Write(obj);
        }
    }
}