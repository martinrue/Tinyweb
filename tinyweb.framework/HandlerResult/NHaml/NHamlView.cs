using NHaml;

namespace tinyweb.framework
{
    public class NHamlView<T> : Template
    {
        public T Model { get; set; }
    }
}