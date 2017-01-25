using Microsoft.Owin.Security.OAuth;
using MoneyManager.Auth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyManager.WebApi.Auth
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider, IAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return base.GrantRefreshToken(context);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //using (var _repo = new AuthRepository(new DataAccess.Services.MapperService()))
            //{
            //    var user = await Task.Run(() => _repo.ValidateUser(context.UserName, context.Password));

            //    if (user == null)
            //    {
            //        context.SetError("invalid_grant", "The user name or password is incorrect.");
            //        return;
            //    }
            //}

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            context.Validated(identity);
        }
    }
}