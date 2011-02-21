namespace tinyweb.framework
{
    public class Route
    {
        public object Defaults { get; set; }
        public string RouteUri { get; set; }

        public Route(string route = null, object defaults = null)
        {
            RouteUri = route;
            Defaults = defaults;
        }
    }
}