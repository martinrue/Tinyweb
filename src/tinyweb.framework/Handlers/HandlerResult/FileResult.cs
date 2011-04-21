using System;
using System.IO;
using Microsoft.Win32;

namespace tinyweb.framework
{
    public class FileResult : IHandlerResult
    {
        string _filepath;
        
        public FileResult(string filepath)
        {
            _filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filepath);

            if (!File.Exists(_filepath))
            {
                throw new FileNotFoundException(String.Format("The file at {0} could not be found", _filepath));
            }
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", Path.GetFileName(_filepath)));
            response.ContentType = getContentType();
            response.WriteFile(_filepath);
        }

        private string getContentType()
        {
            var key = Registry.ClassesRoot.OpenSubKey(Path.GetExtension(_filepath).ToLower());

            if (key != null && key.GetValue("Content Type") != null)
            {
                return key.GetValue("Content Type").ToString();
            }

            return "application/unknown";   
        }
    }
}