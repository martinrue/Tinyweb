using System;
using System.IO;

namespace tinyweb.framework
{
    public class HtmlResult : IResult
    {
        string _filepath;

        public HtmlResult(string filepath)
        {
            _filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filepath);

            if (!File.Exists(_filepath))
            {
                throw new FileNotFoundException(String.Format("The view at {0} could not be found", _filepath));
            }
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.ContentType = "text/html";
            response.Write(File.ReadAllText(_filepath));
        }
    }
}