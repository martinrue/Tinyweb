using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace tinyweb.framework
{
    public class XmlResult : IHandlerResult
    {
        object data;

        public HandlerResultType ResultType
        {
            get { return HandlerResultType.Render; }
        }

        public IDictionary<string, string> CustomHeaders
        {
            get { return new Dictionary<string, string>(); }
        }

        public string ContentType
        {
            get { return "text/xml"; }
        }

        public XmlResult(object data)
        {
            this.data = data;
        }

        public string GetResult()
        {
            if (data != null)
            {
                var result = new StringBuilder();

                using (var writer = new StringWriter(result))
                {
                    new XmlSerializer(this.data.GetType()).Serialize(writer, this.data);
                }

                return result.ToString();
            }

            return String.Empty;
        }
    }
}