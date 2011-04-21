using System.Collections.Generic;

namespace tinyweb.framework
{
    public interface IFilterScanner
    {
        IEnumerable<FilterData> FindAll();
    }
}