using System;
using System.Collections.Generic;

namespace tinyweb.framework
{
    public class ScanResult
    {
        public IEnumerable<Type> Handlers { get; set; }
        public IEnumerable<Type> Filters { get; set; }
    }
}