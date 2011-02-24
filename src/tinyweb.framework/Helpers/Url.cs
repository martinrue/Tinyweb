using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace tinyweb.framework.Helpers
{
    public static class Url
    {
        public static string For<T>(object arguments = null)
        {
            var handler = Tinyweb.Handlers.SingleOrDefault(h => h.Type == typeof(T));

            if (handler == null)
            {
                throw new Exception("The handler {0} was not found".With(typeof(T).Name));
            }

            return "/" + (arguments == null ? handler.Uri : substituteUrlParameters(handler.Uri, arguments));
        }

        private static string substituteUrlParameters(string url, object arguments)
        {
            var properties = arguments.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var unreplaced = new NameValueCollection();

            foreach (var property in properties)
            {
                var name = property.Name;
                var value = property.GetValue(arguments, null).ToString();
                
                var propertyReplaced = false;

                url = recursiveReplace(url, name, value, ref propertyReplaced);

                if (!propertyReplaced)
                {
                    unreplaced.Add(name, value);
                }
            }

            return unreplaced.Count == 0 ? url : url + buildQueryString(unreplaced);
        }

        private static string recursiveReplace(string url, string field, string value, ref bool propertyReplaced)
        {
            while (url.Contains("{{{0}}}".With(field)))
            {
                url = url.Replace("{{{0}}}".With(field), "{0}".With(value.UrlEncode()));
                propertyReplaced = true;
            }

            return url;
        }

        private static string buildQueryString(NameValueCollection parameters)
        {
            var pairs = parameters.AllKeys.Select(key => "{0}={1}".With(key.UrlEncode(), parameters[key].UrlEncode()));

            return "?" + String.Join("&", pairs);
        }
    }
}