using Firefly.AuthManager.Users.Authenticate;
using FluentValidation;

namespace Firefly.AuthManager.Api.Users
{
    public class AuthenticateUserRequestValidator : AbstractValidator<IAuthenticateUserRequest>
    {
        public AuthenticateUserRequestValidator()
        {
            RuleFor(r => r.Username)
                .NotNull()
                .NotEmpty();
            RuleFor(r => r.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
