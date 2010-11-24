using System;

namespace tinyweb.framework
{
    public class Accept : Attribute
    {
        public string AcceptUri { get; set; }

        public Accept(string acceptUri)
        {
            AcceptUri = acceptUri;
        }
    }
}