using System;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Demo.Web
{
    public static class WebApiConfig
    {
       

        public static void Configure(HttpConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            RegisterRoutes(config);
            ConfigureSerializer(config);
           // RegisterFilters(config);
            RemoveUnnecessaryFormatters(config);
        }

        private static void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }

        private static void ConfigureSerializer(HttpConfiguration config)
        {
            var serializerSettings = config.Formatters.JsonFormatter.SerializerSettings;

            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        }

       

        private static void RemoveUnnecessaryFormatters(
            HttpConfiguration config)
        {
            var formatters = config.Formatters;
            var unnecessaryFormatters = formatters.Where(f => f != formatters.JsonFormatter)
                .ToList();

            foreach (var formatter in unnecessaryFormatters)
            {
                formatters.Remove(formatter);
            }
        }
    }
}
