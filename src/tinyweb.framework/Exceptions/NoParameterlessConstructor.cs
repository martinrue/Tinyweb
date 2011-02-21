using System;

namespace tinyweb.framework
{
    public class NoParameterlessConstructorException : Exception
    {
        public NoParameterlessConstructorException(string message) : base(message)
        {

        }
    }
}