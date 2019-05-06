using FluentValidation;
using System.Linq;

namespace Firefly.AuthManager.Users.Store
{
    public class UsernamePasswordPolicyValidator : AbstractValidator<StoreUserCommand>, IUsernamePasswordPolicyValidator
    {
        public UsernamePasswordPolicyValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(r => r.Username)
                .NotNull()
                    .WithMessage("Username must be provided")
                .NotEmpty()
                    .WithMessage("Username must be provided")
                .MinimumLength(3)
                    .WithMessage("Minimum username length is 3")
                .MaximumLength(128)
                    .WithMessage("Maximum username length is 128")
                .Matches(@"^[0-9a-zA-Z ]+$")
                    .WithMessage("Username must be alphanumeric");
            RuleFor(r => r.Password)
                .NotNull()
                    .WithMessage("Password must be provided")
                .NotEmpty()
                    .WithMessage("Password must be provided")
                .MinimumLength(8)
                    .WithMessage("Minimum password length is 8")
                .MaximumLength(30)
                    .WithMessage("Maximum password length is 30");
        }

        public void ThrowExceptionIfInvalid(StoreUserCommand command)
        {
            var result = base.Validate(command);

            if (!result.IsValid)
                throw new InvalidUsernameOrPasswordException(result.Errors.Select(e=>e.ErrorMessage));
        }
    }
}
