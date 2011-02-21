using System;

namespace tinyweb.framework
{
    public class HandlerData
    {
        public Type Type { get; set; }
        public string Uri { get; set; }
        public object DefaultRouteValues { get; set; }
    }
}