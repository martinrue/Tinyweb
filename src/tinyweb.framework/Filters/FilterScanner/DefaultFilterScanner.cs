using System;
using System.Collections.Generic;
using System.Linq;

namespace tinyweb.framework
{
    public class DefaultFilterScanner : IFilterScanner
    {
        public IEnumerable<FilterData> FindAll(IEnumerable<Type> types)
        {
            return findFilters(types);
        }

        public Func<Type, bool> GetSearcher()
        {
            return t => t.Name.ToLower().EndsWith("filter");
        }

        private IEnumerable<FilterData> findFilters(IEnumerable<Type> types)
        {
            var filters = new List<FilterData>();

            foreach (var type in types)
            {
                var methods = type.GetMethods();

                var filter = new FilterData
                {
                    Type = type,
                    Priority = getPriority(type),
                    BeforeFilter = methods.Any(m => m.Name.ToLower() == "before"),
                    AfterFilter = methods.Any(m => m.Name.ToLower() == "after")
                };

                if (filter.AfterFilter || filter.BeforeFilter)
                {
                    filters.Add(filter);
                }
            }

            return filters;
        }

        public int getPriority(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(FilterPriority), false);
            
            if (attributes.Count() >= 1)
            {
                var priority = (FilterPriority)attributes.First();
                return priority.Priority;
            }

            return Int32.MaxValue;
        }
    }
}