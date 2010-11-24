namespace tinyweb.framework
{
    public class HtmlResult : IHandlerResult
    {
        private string data;

        public string ContentType
        {
            get { return "text/html"; }
        }

        public HtmlResult(string data)
        {
            this.data = data;
        }

        public static implicit operator HtmlResult(string input)
        {
            return new HtmlResult(input);
        }

        public string GetResult()
        {
            return data;
        }
    }
}