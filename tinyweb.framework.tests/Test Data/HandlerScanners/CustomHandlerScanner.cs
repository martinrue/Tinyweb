using System;
using System.Collections.Generic;

namespace tinyweb.framework.tests
{
    public class CustomHandlerScanner : IHandlerScanner
    {
        public IEnumerable<HandlerData> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}