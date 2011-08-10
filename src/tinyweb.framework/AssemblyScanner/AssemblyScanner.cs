using System;
using System.Collections.Generic;
using System.Linq;

namespace tinyweb.framework
{
    public class AssemblyScanner
    {
        public static ScanResult FindHandlersAndFilters(Func<Type, bool> isValidHandler, Func<Type, bool> isValidFilter)
        {
            var handlers = new List<Type>();
            var filters = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GlobalAssemblyCache) continue;

                var types = assembly.GetTypes();

                handlers.AddRange(types.Where(isValidHandler));
                filters.AddRange(types.Where(isValidFilter).ToList());
            }

            return new ScanResult { Handlers = handlers, Filters = filters };
        }
    }
}