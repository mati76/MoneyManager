using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;
using System;

namespace MoneyManager.Business
{
    public interface IAuthRepository : IRepository, IDisposable
    {
        AppUser ValidateUser(string userName, string password);

        ActionResult RegisterUser(AppUser user);
    }
}