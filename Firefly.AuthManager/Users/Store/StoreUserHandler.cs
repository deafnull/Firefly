using Firefly.AuthManager.Users.Security;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Firefly.AuthManager.Users.Store
{
    internal class StoreUserHandler : IRequestHandler<StoreUserCommand, IStoreUserResponse>
    {
        private readonly IUsersRepository usersRepository;
        private readonly IUsernamePasswordPolicyValidator usernamePasswordValidator;
        private readonly IPasswordHasher passwordHasher;
        private readonly ILogger logger;

        public StoreUserHandler(IUsersRepository usersRepository, IUsernamePasswordPolicyValidator usernamePasswordValidator, IPasswordHasher passwordHasher, ILogger logger)
        {
            this.usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            this.usernamePasswordValidator = usernamePasswordValidator ?? throw new ArgumentNullException(nameof(usernamePasswordValidator));
            this.passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            this.logger = logger?.ForContext<StoreUserHandler>() ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IStoreUserResponse> Handle(StoreUserCommand command, CancellationToken token)
        {
            try
            {
                usernamePasswordValidator.ThrowExceptionIfInvalid(command);

                var hashedPassword = passwordHasher.Hash(command.Password);

                var user = new UserToStore(command, hashedPassword);

                await usersRepository.Insert(user);

                string successMessage = $"Successfully stored user.";
                logger.ForContext(nameof(command.Username), command.Username)
                    .Information(successMessage);
                return new StoreUserResponse(true, successMessage);
            }
            catch (InvalidUsernameOrPasswordException ex)
            {
                logger.ForContext(nameof(command.Username), command.Username)
                    .Information(ex.Message);
                return new StoreUserResponse(false, ex.Message);
            }
            catch (UserAlreadyExistsException ex)
            {
                logger.ForContext(nameof(command.Username), command.Username)
                    .Information(ex.Message);
                return new StoreUserResponse(false, ex.Message);
            }
            catch (Exception ex)
            {
                string message = $"Error occurred while storing user {command.Username}.";
                logger.ForContext(nameof(command.Username), command.Username)
                    .Error(ex, message);
                return new StoreUserResponse(false, ex.Message);
            }
        }
    }
}
