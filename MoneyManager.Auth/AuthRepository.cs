using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MoneyManager.Business;
using MoneyManager.Business.Models;
using System;

namespace MoneyManager.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapperService _mapperService;
        private readonly AuthContext _ctx;

        public AuthRepository(IMapperService mapperService)
        {
            if (mapperService == null)
            {
                throw new ArgumentNullException("mapperService");
            }

            _mapperService = mapperService;
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public AppUser ValidateUser(string userName, string password)
        {
            return _mapperService.Map<AppUser>(_userManager.Find(userName, password));
        }

        public ActionResult RegisterUser(AppUser user)
        {
            return _mapperService.Map<ActionResult>(_userManager.Create(_mapperService.Map<IdentityUser>(user), user.Password));
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
