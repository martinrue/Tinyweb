namespace tinyweb.framework.tests
{
    public class InvalidFilter
    {
        public IResult Unrecognised()
        {
            return new NoneResult();
        }
    }
}