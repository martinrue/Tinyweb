using System;
using System.Collections.Generic;
using System.IO;

namespace tinyweb.framework
{
    public class HtmlResult : IHandlerResult
    {
        string filepath;

        public IDictionary<string, string> CustomHeaders
        {
            get { return new Dictionary<string, string>(); }
        }

        public string ContentType
        {
            get { return "text/html"; }
        }

        public bool IsFileResult
        {
            get { return false; }
        }

        public HtmlResult(string filepath)
        {
            this.filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filepath);

            if (!File.Exists(this.filepath))
            {
                throw new FileNotFoundException(String.Format("The view at {0} could not be found", this.filepath));
            }
        }

        public static implicit operator HtmlResult(string filepath)
        {
            return new HtmlResult(filepath);
        }

        public string GetResult()
        {
            return File.ReadAllText(this.filepath);
        }
    }
}