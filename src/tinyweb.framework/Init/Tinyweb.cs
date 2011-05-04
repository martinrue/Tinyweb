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
        public static IEnumerable<FilterData> Filters { get; set; }

        public static Action<Exception, RequestContext, HandlerData> OnError;
        public static bool AllowFormatExtensions { get; set; }

        public static int Init(params Registry[] registries)
        {
            ObjectFactory.Initialize(x => registries.ForEach(x.AddRegistry));

            Filters = FilterScanner.Current.FindAll();
            Handlers = HandlerScanner.Current.FindAll();
            Handlers.ForEach(addRoute);

            return Handlers.Count();
        }

        public static string WhatHaveIGot()
        {
            var report = new StringBuilder();

            report.AppendLine("Routes:");

            foreach (var handler in Handlers.OrderBy(h => h.Uri))
            {
                report.AppendLine(String.Format("/{0} -> {1}", handler.Uri, handler.Type.Name));
            }

            report.AppendLine("Filters:");

            foreach (var filter in Filters.Where(f => f.BeforeFilter).OrderBy(f => f.Priority))
            {
                report.AppendLine(String.Format("Before: {0} [Priority {1}]", filter.Type.Name, filter.Priority));
            }

            foreach (var filter in Filters.Where(f => f.AfterFilter).OrderBy(f => f.Priority))
            {
                report.AppendLine(String.Format("After: {0} [Priority {1}]", filter.Type.Name, filter.Priority));
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