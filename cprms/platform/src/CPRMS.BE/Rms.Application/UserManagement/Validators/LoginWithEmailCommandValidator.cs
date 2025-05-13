using Core.Application.Validators;
using Rms.Application.UserManagement.Commands.LoginWithEmail;

namespace Rms.Application.UserManagement.Validators
{
    public class LoginWithEmailCommandValidator : BaseValidator<LoginWithEmailCommand> 
    {
        public LoginWithEmailCommandValidator()
        {

            //RuleFor(x => x.Email)
            //    .NotEmpty().WithMessage("Email.")
            //    .EmailAddress().WithMessage("Email.");
        }
    }
}
