using System;
using tinyweb.framework;

namespace tinyweb.sample.Handlers
{
    [Accept("users/register")]
    public class HomeHandler
    {
        object defaults = new { n2 = 10 };

        public HtmlResult Post(UserRegistrationModel model)
        {
            return String.Format("Welcome {0}, you {1} spam and your favourite color is {2}", model.Name, model.Spam, model.Colour);
        }
    }

    public class UserRegistrationModel
    {
        public string Name { get; set; }
        public bool Spam { get; set; }
        public string Colour { get; set; }
    }
}