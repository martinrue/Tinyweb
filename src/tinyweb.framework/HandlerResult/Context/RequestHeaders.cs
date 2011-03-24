using System.Web;

namespace tinyweb.framework
{
    public class RequestHeaders : IRequestHeaders
    {
        HttpContext _context;

        public RequestHeaders(HttpContext context)
        {
            _context = context;
        }

        public string this[string header]
        {
            get { return _context.Request.Headers[header]; }
        }
    }
}