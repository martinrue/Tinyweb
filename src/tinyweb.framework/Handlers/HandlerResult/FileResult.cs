using System;
using System.IO;
using Microsoft.Win32;

namespace tinyweb.framework
{
    public class FileResult : IResult
    {
        public string FilePath { get; set; }

        public FileResult(string filepath)
        {
            FilePath = filepath;
        }

        public void ProcessResult(IRequestContext request, IResponseContext response)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException(String.Format("The file at {0} could not be found", fullPath));
            }

            response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", Path.GetFileName(fullPath)));
            response.ContentType = getContentType(fullPath);
            response.WriteFile(fullPath);
        }

        private string getContentType(string path)
        {
            var key = Registry.ClassesRoot.OpenSubKey(Path.GetExtension(path).ToLower());

            if (key != null && key.GetValue("Content Type") != null)
            {
                return key.GetValue("Content Type").ToString();
            }

            return "application/unknown";   
        }
    }
}