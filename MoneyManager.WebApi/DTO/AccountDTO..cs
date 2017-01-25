
using FluentValidation.Attributes;
using MoneyManager.WebApi.Validation;

namespace MoneyManager.WebApi.DTO
{
    //[Validator(typeof(AccountValidator))]
    public class AccountDTO
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
    }
}