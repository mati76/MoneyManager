using FluentValidation.WebApi;
using MoneyManager.WebApi.Filters;
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
            config.Filters.Add(new ValidateModelStateFilter());

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            config.DependencyResolver = new UnityDependencyResolver(ContainerConfig.CofigureContainer());
            FluentValidationModelValidatorProvider.Configure(config);
            return config;
        }
    }
}