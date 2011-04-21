using System;

namespace tinyweb.framework
{
    public class RunPriority : Attribute
    {
        public int Priority { get; set; }

        public RunPriority(int priority)
        {
            Priority = priority;
        }
    }
}