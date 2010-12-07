using System.Collections.Generic;

namespace tinyweb.framework
{
    public interface IHandlerResult
    {
        IDictionary<string, string> CustomHeaders { get; }
        string ContentType { get; }
        bool IsFileResult { get; }
        string GetResult();
    }
}