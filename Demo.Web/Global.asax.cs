﻿using System.Web.Http;

namespace Demo.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Configure);
        }
    }
}
