using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace tinyweb.framework
{
    public class XmlResult : IResult
    {
        public object Data { get; set; }

        public XmlResult(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            Data = data;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "application/xml";

            var result = new StringBuilder();

            using (var writer = new StringWriter(result))
            {
                new XmlSerializer(Data.GetType()).Serialize(writer, Data);
            }

            response.Write(result.ToString());
        }
    }
}