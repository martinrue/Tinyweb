using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace tinyweb.framework
{
    public class XmlResult : IResult
    {
        object _data;

        public XmlResult(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            _data = data;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "application/xml";

            var result = new StringBuilder();

            using (var writer = new StringWriter(result))
            {
                new XmlSerializer(_data.GetType()).Serialize(writer, _data);
            }

            response.Write(result.ToString());
        }
    }
}