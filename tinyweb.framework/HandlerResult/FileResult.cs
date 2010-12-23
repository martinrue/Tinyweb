using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace tinyweb.framework
{
    public class FileResult : IHandlerResult
    {
        string filepath;

        public HandlerResultType ResultType
        {
            get { return HandlerResultType.Download; }
        }

        public IDictionary<string, string> CustomHeaders
        {
            get
            {
                return new Dictionary<string, string> 
                { 
                    {"Content-Disposition", String.Format("attachment; filename={0}", Path.GetFileName(this.filepath))} 
                };
            }
        }

        public string ContentType
        {
            get 
            {
                var key = Registry.ClassesRoot.OpenSubKey(Path.GetExtension(this.filepath).ToLower());

                if (key != null && key.GetValue("Content Type") != null)
                {
                    return key.GetValue("Content Type").ToString();
                }

                return "application/unknown";
            }
        }

        public FileResult(string filepath)
        {
            this.filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filepath);

            if (!File.Exists(this.filepath))
            {
                throw new FileNotFoundException(String.Format("The file at {0} could not be found", this.filepath));
            }
        }

        public string GetResult()
        {
            return this.filepath;
        }       
    }
}