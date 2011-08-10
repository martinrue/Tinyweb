using System;
using System.Collections.Generic;

namespace tinyweb.framework.tests
{
    public class CustomHandlerScanner : IHandlerScanner
    {
        public IEnumerable<HandlerData> FindAll(IEnumerable<Type> types)
        {
            throw new NotImplementedException();
        }

        public Func<Type, bool> GetSearcher()
        {
            throw new NotImplementedException();
        }
    }
}