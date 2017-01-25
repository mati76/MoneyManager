using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace MoneyManager.WebApi.Auth
{
    public interface IAuthorizationServerProvider
    {
        Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context);
        Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context);
    }
}