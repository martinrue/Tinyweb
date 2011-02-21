using System.Collections.Generic;
using tinyweb.framework;
using tinyweb.viewengine.ndjango;

namespace tinyweb.samples.ndjango.Handlers
{
    public class HomeHandler
    {
        public IHandlerResult Get()
        {
            var model = new List<string> { "item 1", "item 2", "item 3", "item 4", "item 5" };

            return View.NDjango(model, "Views\\Index.django");
        }
    }
}