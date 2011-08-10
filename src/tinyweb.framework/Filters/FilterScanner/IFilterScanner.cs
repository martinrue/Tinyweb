using System;
using System.Collections.Generic;

namespace tinyweb.framework
{
    public interface IFilterScanner
    {
        IEnumerable<FilterData> FindAll(IEnumerable<Type> types);
        Func<Type, bool> GetSearcher();
    }
}