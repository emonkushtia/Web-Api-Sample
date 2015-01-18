using System;
using System.Web.Routing;

namespace Demo.Core.ExtensionMethods
{
    public static class RouteValueDictionaryExtensions
    {
        public static string Controller(this RouteValueDictionary instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            return (string)instance["controller"];
        }

        public static string Action(this RouteValueDictionary instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            return (string)instance["action"];
        }
    }
}