using System;

namespace tinyweb.framework
{
    public class FilterPriority : Attribute
    {
        public int Priority { get; set; }

        public FilterPriority(int priority)
        {
            Priority = priority;
        }
    }
}