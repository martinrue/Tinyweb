using System.Web;

namespace tinyweb.framework
{
    public class DefaultRequestContext : IRequestContext
    {
        public IRequestHeaders Headers { get; private set; }

        public DefaultRequestContext(HttpContext context)
        {
            Headers = new RequestHeaders(context);
        }
    }
}