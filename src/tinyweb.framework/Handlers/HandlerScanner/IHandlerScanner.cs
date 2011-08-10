using System;
using System.Collections.Generic;

namespace tinyweb.framework
{
    public interface IHandlerScanner
    {
        IEnumerable<HandlerData> FindAll(IEnumerable<Type> types);
        Func<Type, bool> GetSearcher();
    }
}