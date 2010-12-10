using tinyweb.framework;

namespace tinyweb.samples.formpost.Handlers
{
    public class UserRegistrationHandler
    {
        Route route = new Route("users/register");

        public IHandlerResult Get()
        {
            return Result.Spark("Views\\Register.spark", "Master.spark");
        }

        public IHandlerResult Post(UserRegistrationModel model)
        {
            return Result.Spark(model, "Views\\Complete.spark", "Master.spark");
        }
    }
}