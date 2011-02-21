using tinyweb.framework;
using tinyweb.viewengine.spark;

namespace tinyweb.samples.formpost.Handlers
{
    public class UsersRegisterHandler
    {
        public IHandlerResult Get()
        {
            return View.Spark("Views/Register.spark", "Master.spark");
        }

        public IHandlerResult Post(UserRegistrationModel model)
        {
            return View.Spark(model, "Views/Complete.spark", "Master.spark");
        }
    }
}