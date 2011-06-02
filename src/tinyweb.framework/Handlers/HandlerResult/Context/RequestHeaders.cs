using System.Linq;
using System.Web;

namespace tinyweb.framework
{
    public class RequestHeaders : IRequestHeaders
    {
        HttpContextBase _context;

        public RequestHeaders(HttpContextBase context)
        {
            _context = context;
        }

        public string this[string header]
        {
            get { return _context.Request.Headers[header]; }
        }

        public bool KeyExists(string key)
        {
            return _context.Request.Headers.AllKeys.Any(k => k == key);
        }
    }
}