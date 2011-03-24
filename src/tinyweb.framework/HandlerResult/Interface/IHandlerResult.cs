namespace tinyweb.framework
{
    public interface IHandlerResult
    {
        void ProcessResult(IRequestContext request, IResponseContext response);
    }
}