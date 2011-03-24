using System.Web;

namespace tinyweb.framework
{
    public class DefaultResponseContext : IResponseContext
    {
        HttpContext _context;

        public DefaultResponseContext(HttpContext context)
        {
            _context = context;
        }

        public string ContentType
        {
            get { return _context.Response.ContentType; }
            set { _context.Response.ContentType = value; }
        }

        public void AddHeader(string name, string value)
        {
            _context.Response.AddHeader(name, value);
        }

        public void Write(string data)
        {
            _context.Response.Write(data);
        }

        public void WriteFile(string data)
        {
            _context.Response.WriteFile(data);
        }

        public void Redirect(string url)
        {
            _context.Response.Redirect(url);
        }
    }
}