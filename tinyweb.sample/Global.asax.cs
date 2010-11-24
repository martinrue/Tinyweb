using System;
using System.Web;
using tinyweb.framework;

namespace tinyweb.sample
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Tinyweb.Initialise();
        }
    }
}