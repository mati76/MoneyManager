using MoneyManager.Api.DTO;
using MoneyManager.Business.Models;

namespace MoneyManager.Api.Services
{
    public interface IAccountService
    {
        ActionResult Register(AccountDTO account);
    }
}