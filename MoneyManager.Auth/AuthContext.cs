using Microsoft.AspNet.Identity.EntityFramework;

namespace MoneyManager.Auth
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("MoneyManager")
        {

        }
    }
}
