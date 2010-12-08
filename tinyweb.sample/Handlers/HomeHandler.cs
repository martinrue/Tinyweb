using System.Collections.Generic;
using tinyweb.framework;

namespace tinyweb.sample.Handlers
{
    public class HomeHandler
    {
        public IHandlerResult Get()
        {
            var model = new List<string> { "item 1", "item 2", "item 3", "item 4", "item 5" };

            return Result.Spark(model, "Views\\Index.spark", "Master.spark");
        }
    }
}