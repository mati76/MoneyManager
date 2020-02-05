using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MoneyManager.Api.DTO;
using MoneyManager.Api.Services;
using BLL = MoneyManager.Business.Models;
using System;

namespace MoneyManager.Api.Controllers
{

    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase, IAccountController
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

        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(AccountDTO account)
        {
            var result = _accountService.Register(account);
            if (result.Succeeded)
            {
                return Ok(); //change to Created
            }
            return GetErrorResult(result);
        }

        private IActionResult GetErrorResult(BLL.ActionResult result)
        {
            if (result == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
