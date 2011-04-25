namespace tinyweb.framework
{
    public interface IResult
    {
        void ProcessResult(IRequestContext request, IResponseContext response);
    }
}