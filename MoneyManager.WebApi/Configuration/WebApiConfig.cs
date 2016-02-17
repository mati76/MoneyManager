using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity.WebApi;

namespace MoneyManager.WebApi.Configuration
{
    public static class WebApiConfig
    {
        public static HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            config.DependencyResolver = new UnityDependencyResolver(ContainerConfig.CofigureContainer());
            return config;
        }
    }
}