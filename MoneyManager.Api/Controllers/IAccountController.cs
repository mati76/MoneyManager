using Microsoft.AspNetCore.Mvc;
using MoneyManager.Api.DTO;

namespace MoneyManager.Api.Controllers
{
    public interface IAccountController
    {
        IActionResult Register(AccountDTO account);
    }
}