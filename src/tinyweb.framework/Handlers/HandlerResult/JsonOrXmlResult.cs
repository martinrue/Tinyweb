using System;

namespace tinyweb.framework
{
    public class JsonOrXmlResult : IResult
    {
        public object Data { get; set; }

        public JsonOrXmlResult(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            Data = data;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            if (request.RouteValues.Routes.ContainsKey("format"))
            {
                switch (request.RouteValues.Routes["format"].ToString())
                {
                    case "xml":
                    {
                        new XmlResult(Data).ProcessResult(request, response);
                    }
                    break;

                    default:
                    {
                        new JsonResult(Data).ProcessResult(request, response);
                    }
                    break;
                }
            }
            else
            {
                if (!request.Headers.KeyExists("Accept"))
                {
                    new JsonResult(Data).ProcessResult(request, response);
                    return;
                }

                var accept = request.Headers["Accept"].ParseAcceptHeader();

                var jsonPriority = accept.ContainsKey("application/json") ? accept["application/json"] : 1;
                var xmlPriority = accept.ContainsKey("application/xml") ? accept["application/xml"] : 0.9;

                if (xmlPriority > jsonPriority)
                {
                    new XmlResult(Data).ProcessResult(request, response);
                }
                else
                {
                    new JsonResult(Data).ProcessResult(request, response);
                }
            }
        }
    }
}