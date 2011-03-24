using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace tinyweb.framework
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action(element);
            }
        }

        public static string With(this string input, params object[] args)
        {
            return string.Format(input, args);
        }

        public static T ToEnum<T>(this string input)
        {
            return (T)Enum.Parse(typeof(T), input, true);
        }

        public static string Name<T>(this T enumeration)
        {
            return Enum.GetName(typeof(T), enumeration);
        }

        public static int CastInt<T>(this T enumeration)
        {
            return (int)(object)enumeration;
        }

        public static RouteValueDictionary ToRouteValueDictionary(this NameValueCollection collection)
        {
            var routeValueDictionary = new RouteValueDictionary();

            foreach (var key in collection.AllKeys)
            {
                routeValueDictionary.Add(key, collection[key]);
            }

            return routeValueDictionary;
        }

        public static bool IsEmpty(this string input)
        {
            return String.IsNullOrEmpty(input);
        }

        public static string[] PascalSplit(this string input)
        {
            return input.IsEmpty() ? new string[0] : Regex.Replace(input, "(\\B[A-Z])", " $1").Split(' ');
        }

        public static string UrlEncode(this string input)
        {
            return HttpUtility.UrlEncode(input);
        }

        public static Dictionary<string, double> ParseAcceptHeader(this string acceptHeader)
        {
            var mimeTypes = new Dictionary<string, double>();

            foreach (var token in acceptHeader.Split(',').Select(x => x.Trim()))
            {
                if (token.Contains(";"))
                {
                    var preferenceParts = token.Split(';').Select(x => x.Trim());
                    mimeTypes.Add(preferenceParts.ElementAt(0), Convert.ToDouble(preferenceParts.ElementAt(1).Substring(2)));
                }
                else
                {
                    mimeTypes.Add(token, 1);
                }
            }

            return mimeTypes.OrderByDescending(x => x.Value).ToDictionary(k => k.Key, v => v.Value);
        }
    }
}