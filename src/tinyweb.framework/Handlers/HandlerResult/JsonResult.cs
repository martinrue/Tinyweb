using System;
using System.Web.Script.Serialization;

namespace tinyweb.framework
{
    public class JsonResult : IResult
    {
        object _data;

        public JsonResult(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            _data = data;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "application/json";
            response.Write(new JavaScriptSerializer().Serialize(_data));
        }
    }
}