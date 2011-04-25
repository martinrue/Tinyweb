namespace tinyweb.framework
{
    public class StringResult : IResult
    {
        string _data;

        public StringResult(string data)
        {
            _data = data;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Write(_data);
        }
    }
}