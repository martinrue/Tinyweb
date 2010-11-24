using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace tinyweb.framework
{
    public class DefaultHandlerScanner : IHandlerScanner
    {
        public IEnumerable<HandlerData> FindAll()
        {
            var types = this.findHandlers();

            return types.Where(t => t.GetCustomAttributes(typeof(Accept), false).Length == 1)
                        .Select(type => new HandlerData
                        {  
                            Type = type,
                            Uri = (type.GetCustomAttributes(typeof(Accept), false).First() as Accept).AcceptUri,
                            DefaultRouteValues = getDefaultRouteValues(type)
                        });
        }

        private IEnumerable<Type> findHandlers()
        {
            var typesFound = new List<Type>();

            AppDomain.CurrentDomain.GetAssemblies().ForEach(assembly =>
            {
                typesFound.AddRange(assembly.GetTypes().Where(t => t.Name.ToLower().EndsWith("handler")));
            });

            return typesFound;
        }

        private object getDefaultRouteValues(Type type)
        {
            var field = type.GetField("defaults", BindingFlags.Instance | BindingFlags.NonPublic);

            if (field != null)
            {
                var handlerInstace = HandlerFactory.Current.Create(new HandlerData { Type = type });
                return field.GetValue(handlerInstace);
            }

            return null;
        }
    }
}