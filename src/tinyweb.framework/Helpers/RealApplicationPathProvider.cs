using System.Web;

namespace tinyweb.framework.Helpers
{
    public class RealApplicationPathProvider : IApplicationPathProvider
    {
        public string GetApplicationPath()
        {
            return HttpContext.Current.Request.ApplicationPath;
        }
    }
}