using MoneyManager.Business.Models;

namespace MoneyManager.Business.Interfaces
{
    public interface IAuthBusiness
    {
        ActionResult RegisterUser(AppUser user);

        AppUser ValidateUser(string userName, string password);
    }
}
