namespace Demo.Web
{
    using System;
    using System.Web.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

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
    }
}
