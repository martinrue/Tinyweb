using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace tinyweb.framework
{
    public class JsonResult : IHandlerResult
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
            get { return "text/json"; }
        }

        public JsonResult(object data)
        {
            this.data = data;
        }

        public string GetResult()
        {
            if (data != null)
            {
                return new JavaScriptSerializer().Serialize(this.data);
            }

            return String.Empty;
        }
    }
}