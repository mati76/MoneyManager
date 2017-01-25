using MoneyManager.Business.Models;
using MoneyManager.WebApi.DTO;

namespace MoneyManager.WebApi.Services
{
    public interface IAccountService
    {
        ActionResult Register(AccountDTO account);
    }
}