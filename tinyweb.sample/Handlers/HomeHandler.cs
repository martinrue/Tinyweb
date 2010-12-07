using System.Collections.Generic;
using tinyweb.framework;

namespace tinyweb.sample.Handlers
{
    public class FamilyHandler
    {
        public IHandlerResult Get()
        {
            var model = new List<string> { "Homer", "Marge", "Bart", "Lisa", "Grandpa" };

            return Result.Spark(model, "Views\\Index.spark");
        }
    }
}