using MediatR;
using System;

namespace Firefly.AuthManager.Users.Store
{
    public class StoreUserCommand : IRequest<IStoreUserResponse>
    {
        public StoreUserCommand(IStoreUserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Username = request.Username ?? throw new ArgumentNullException(nameof(Username));
            Password = request.Password ?? throw new ArgumentNullException(nameof(Password));
        }

        public string Username { get; }
        public string Password { get; }
    }
}
