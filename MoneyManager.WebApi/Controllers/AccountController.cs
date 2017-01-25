using MoneyManager.Business.Models;
using MoneyManager.WebApi.DTO;
using MoneyManager.WebApi.Filters;
using MoneyManager.WebApi.Services;
using System;
using System.Web.Http;

namespace MoneyManager.WebApi.Controllers
{
    public class AccountController : ApiController, IAccountController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            if(accountService == null)
            {
                throw new ArgumentNullException(nameof(accountService));
            }
            _accountService = accountService;
        }

        [AllowAnonymous]
        [ValidateModelStateFilter]
        [HttpPost]
        public IHttpActionResult Register(AccountDTO account)
        {
            var result = _accountService.Register(account);
            if (result.Succeeded)
            {
                return Ok();
            }
            return GetErrorResult(result);
        }

        private IHttpActionResult GetErrorResult(ActionResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
