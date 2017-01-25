using Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Cors;
using MoneyManager.WebApi.Configuration;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(MoneyManager.WebApi.Startup))]
namespace MoneyManager.WebApi
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Startup
    {
        // This method is required by Katana:
        public void Configuration(IAppBuilder app)
        {
            // Use the extension method provided by the WebApi.Owin library:
            //app.Use(new Func<AppFunc, AppFunc>(MyMiddleWare));
            app.UseCors(CorsOptions.AllowAll);
            AuthConfig.ConfigureOAuth(app);
            app.UseWebApi(WebApiConfig.ConfigureWebApi());

            
            Services.MapperService.Configure();
        }

        public AppFunc MyMiddleWare(AppFunc next)
        {
            AppFunc appFunc = async (IDictionary<string, object> environment) =>
            {
                // Do something with the incoming request:
                //var response = environment["owin.ResponseBody"] as System.IO.Stream;
                //using (var writer = new StreamWriter(response))
                //{
                //    await writer.WriteAsync("<h1>Hello from My First Middleware</h1>");
                //}
                //// Call the next Middleware in the chain:
                await next.Invoke(environment);
            };
            return appFunc;
        }
    }
}