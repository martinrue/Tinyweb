namespace tinyweb.framework
{
    public interface IHandlerResult : IResult
    {
        
    }

    public interface IResult
    {
        void ProcessResult(IRequestContext request, IResponseContext response);
    }
}