using System.Collections.Generic;
using tinyweb.framework;
using tinyweb.viewengine.nhaml;

namespace tinyweb.samples.nhaml
{
    public class HomeHandler
    {
        public IHandlerResult Get()
        {
            var model = new List<string> { "item 1", "item 2", "item 3", "item 4", "item 5" };

            return View.NHaml(model, "Views/Index.haml");
        }
    }
}