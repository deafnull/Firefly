using Firefly.AuthManager.Users.Security;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Firefly.AuthManager.Users.Authenticate
{
    internal class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, IAuthenticateUserResponse>
    {
        private readonly IUsersRepository usersRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly ILogger logger;

        public AuthenticateUserHandler(IUsersRepository usersRepository, IPasswordHasher passwordHasher, ILogger logger)
        {
            this.usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            this.passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            this.logger = logger?.ForContext<AuthenticateUserHandler>() ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IAuthenticateUserResponse> Handle(AuthenticateUserCommand command, CancellationToken token)
        {
            try
            {
                var user = await usersRepository.Get(command.Username);

                var validPassword = passwordHasher.IsValid(user.Hash, user.Salt, command.Password);

                return new AuthenticateUserResponse(validPassword, validPassword ? "Success" : "Invalid password");
            }
            catch (Exception ex)
            {
                logger.ForContext(nameof(command.Username), command.Username)
                    .Error(ex, $"Error occurred while authenticating user {command.Username}");
                return new AuthenticateUserResponse(false, ex.Message);
            }
        }
    }
}
