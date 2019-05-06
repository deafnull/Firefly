using FluentValidation;
using Firefly.AuthManager.Users.Store;

namespace Firefly.AuthManager.Api.Users
{
    public class StoreUserRequestValidator : AbstractValidator<IStoreUserRequest>
    {
        public StoreUserRequestValidator()
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
