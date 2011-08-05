namespace tinyweb.framework
{
    public class StringResult : IResult
    {
        public string Data { get; set; }

        public StringResult(string data)
        {
            Data = data;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Write(Data);
        }
    }
}