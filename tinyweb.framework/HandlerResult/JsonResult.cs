using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace tinyweb.framework
{
    public class JsonResult : IHandlerResult
    {
        object data;

        public IDictionary<string, string> CustomHeaders
        {
            get { return new Dictionary<string, string>(); }
        }

        public string ContentType
        {
            get { return "text/json"; }
        }

        public bool IsFileResult
        {
            get { return false; }
        }

        public JsonResult(object data)
        {
            this.data = data;
        }

        public string GetResult()
        {
            if (data != null)
            {
                var serialiser = new JavaScriptSerializer();
                return serialiser.Serialize(this.data);
            }

            return String.Empty;
        }
    }
}