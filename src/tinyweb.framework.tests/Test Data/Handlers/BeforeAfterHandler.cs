using System.Collections.Generic;

namespace tinyweb.framework.tests
{
    public class BeforeAfterHandler
    {
        public List<string> Calls = new List<string>();

        public void Before()
        {
            Calls.Add("before");
        }

        public IHandlerResult Get()
        {
            Calls.Add("get");

            return new StringResult("");
        }

        public void After()
        {
            Calls.Add("after");
        }
    }
}