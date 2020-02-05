using FluentValidation;
using MoneyManager.Api.DTO;

namespace MoneyManager.Api.Validation
{
    public class AccountValidator : AbstractValidator<AccountDTO>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter password.").
                Matches(@"^.*(?=.{8,})(?=.*[\d])(?=.*[\W]).*$").WithMessage("Password does not meet following requirements: 1) minimum 8 characters lenght 2) at least one special character 3) at least one digit");
            RuleFor(x => x.ConfirmPassword).Must((AccountDTO x, string y) => Equals(x.Password, x.ConfirmPassword)).WithMessage("Confirmed password does not match entered password");
        }
    }
}