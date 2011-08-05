using System;
using System.Web.Script.Serialization;

namespace tinyweb.framework
{
    public class JsonResult : IResult
    {
        public object Data { get; set; }

        public JsonResult(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            Data = data;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "application/json";
            response.Write(new JavaScriptSerializer().Serialize(Data));
        }
    }
}