using System.Collections.Generic;

namespace tinyweb.framework
{
    public interface IHandlerScanner
    {
        IEnumerable<HandlerData> FindAll();
    }
}