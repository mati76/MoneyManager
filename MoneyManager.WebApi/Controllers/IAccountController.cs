using System.Web.Http;
using MoneyManager.WebApi.DTO;

namespace MoneyManager.WebApi.Controllers
{
    public interface IAccountController
    {
        IHttpActionResult Register(AccountDTO account);
    }
}