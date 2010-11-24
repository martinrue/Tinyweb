using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace tinyweb.framework
{
    public static class Tinyweb
    {
        public static IEnumerable<HandlerData> Handlers { get; set; }

        public static int Initialise()
        {
            Handlers = HandlerScanner.Current.FindAll();

            Handlers.ForEach(handler =>
            {
                RouteTable.Routes.Add(new Route(handler.Uri, new RouteValueDictionary(handler.DefaultRouteValues) , new RouteHandler(handler)));
            });

            return Handlers.Count();
        }
    }
}