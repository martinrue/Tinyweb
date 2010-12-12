using System.Collections.Generic;

namespace tinyweb.framework
{
    public interface IHandlerResult
    {
        HandlerResultType ResultType { get; }
        IDictionary<string, string> CustomHeaders { get; }
        string ContentType { get; }
        string GetResult();
    }
}