using System;

namespace tinyweb.framework
{
    public class JsonOrXmlResult : IResult
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
            if (request.RouteValues.Routes.ContainsKey("format"))
            {
                switch (request.RouteValues.Routes["format"].ToString())
                {
                    case "xml":
                    {
                        new XmlResult(_data).ProcessResult(request, response);
                    }
                    break;

                    default:
                    {
                        new JsonResult(_data).ProcessResult(request, response);
                    }
                    break;
                }
            }
            else
            {
                if (!request.Headers.KeyExists("Accept"))
                {
                    new JsonResult(_data).ProcessResult(request, response);
                    return;
                }

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
}