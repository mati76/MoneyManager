using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using MoneyManager.WebApi.Auth;
using Owin;
using System;

namespace MoneyManager.WebApi.Configuration
{
    public class AuthConfig
    {
        public static void ConfigureOAuth(IAppBuilder app)
        {
            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/auth"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}