using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using StructureMap;
using StructureMap.Configuration.DSL;
using SWR = System.Web.Routing;

namespace tinyweb.framework
{
    public static class Tinyweb
    {
        public static IEnumerable<HandlerData> Handlers { get; set; }

        public static bool AllowFormatExtensions { get; set; }

        public static int Init(params Registry[] registries)
        {
            ObjectFactory.Initialize(x => registries.ForEach(x.AddRegistry));

            Handlers = HandlerScanner.Current.FindAll();
            Handlers.ForEach(addRoute);

            return Handlers.Count();
        }

        public static string WhatHaveIGot()
        {
            var report = new StringBuilder();

            foreach (var handler in Handlers.OrderBy(h => h.Uri))
            {
                report.AppendLine(String.Format("/{0} -> {1}", handler.Uri, handler.Type.Name));
            }

            return report.ToString();
        }

        private static void addRoute(HandlerData handler)
        {
            RouteTable.Routes.Add(new SWR.Route(handler.Uri.Trim('/'), new RouteValueDictionary(handler.DefaultRouteValues), new RouteHandler(handler)));

            if (AllowFormatExtensions)
            {
                var route = handler.Uri.Trim('/') + ".{format}";

                RouteTable.Routes.Add(new SWR.Route(route, new RouteValueDictionary(handler.DefaultRouteValues), new RouteHandler(handler)));
            }
        }
    }
}