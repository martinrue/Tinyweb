using System;

namespace tinyweb.framework
{
    public class JsonOrXmlResult : IHandlerResult
    {
        object _data;

        public JsonOrXmlResult(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            _data = data;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            var accept = request.Headers["Accept"].ParseAcceptHeader();

            var jsonPriority = accept.ContainsKey("application/json") ? accept["application/json"] : 1;
            var xmlPriority = accept.ContainsKey("application/xml") ? accept["application/xml"] : 0.9;

            if (xmlPriority > jsonPriority)
            {
                new XmlResult(_data).ProcessResult(request, response);
            }
            else
            {
                new JsonResult(_data).ProcessResult(request, response);
            }
        }
    }
}