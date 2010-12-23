using NHaml;

namespace tinyweb.viewengine.nhaml
{
    public class NHamlView<T> : Template
    {
        public T Model { get; set; }
    }
}