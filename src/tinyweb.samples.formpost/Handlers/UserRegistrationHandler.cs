using tinyweb.framework;
using tinyweb.viewengine.spark;

namespace tinyweb.samples.formpost.Handlers
{
    public class UserRegistrationHandler
    {
        Route route = new Route("users/register");

        public IHandlerResult Get()
        {
            return View.Spark("Views\\Register.spark", "Master.spark");
        }

        public IHandlerResult Post(UserRegistrationModel model)
        {
            return View.Spark(model, "Views\\Complete.spark", "Master.spark");
        }
    }
}