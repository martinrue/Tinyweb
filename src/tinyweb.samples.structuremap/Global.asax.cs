using System;
using System.Web;
using tinyweb.framework;

namespace tinyweb.samples.structuremap
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Tinyweb.Init(new ServiceRegistry());
        }
    }
}