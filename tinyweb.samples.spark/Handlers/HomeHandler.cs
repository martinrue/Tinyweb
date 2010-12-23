using System.Collections.Generic;
using tinyweb.framework;
using tinyweb.viewengine.spark;

namespace tinyweb.samples.spark
{
    public class HomeHandler
    {
        public IHandlerResult Get()
        {
            var model = new List<string> { "item 1", "item 2", "item 3", "item 4", "item 5" };

            return View.Spark(model, "Views\\Index.spark", "Master.spark");
        }
    }
}