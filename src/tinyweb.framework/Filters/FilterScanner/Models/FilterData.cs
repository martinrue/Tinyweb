using System;

namespace tinyweb.framework
{
    public class FilterData
    {
        public Type Type { get; set; }
        public int Priority { get; set; }
        public bool BeforeFilter { get; set; }
        public bool AfterFilter { get; set; }
    }
}