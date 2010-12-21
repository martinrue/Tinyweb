using System.Collections.Generic;
using tinyweb.framework;

namespace tinyweb.samples.nhaml
{
    public class HomeHandler
    {
        public IHandlerResult Get()
        {
            var model = new List<string> { "item 1", "item 2", "item 3", "item 4", "item 5" };

            return Result.NHaml(model, "Views\\Index.haml");
        }
    }
}