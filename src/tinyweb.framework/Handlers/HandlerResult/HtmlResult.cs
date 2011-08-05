using System;
using System.IO;

namespace tinyweb.framework
{
    public class HtmlResult : IResult
    {
        public string FilePath { get; set; }

        public HtmlResult(string filepath)
        {
            FilePath = filepath;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException(String.Format("The view at {0} could not be found", fullPath));
            }

            response.ContentType = "text/html";
            response.Write(File.ReadAllText(fullPath));
        }
    }
}