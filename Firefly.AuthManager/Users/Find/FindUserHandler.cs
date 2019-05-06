using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Firefly.AuthManager.Users.Find
{
    internal class FindUserHandler : IRequestHandler<FindUserQuery, IFindUserResponse>
    {
        private readonly IUsersRepository usersRepository;
        private readonly ILogger logger;

        public FindUserHandler(IUsersRepository usersRepository, ILogger logger)
        {
            this.usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            this.logger = logger?.ForContext<FindUserHandler>() ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IFindUserResponse> Handle(FindUserQuery command, CancellationToken token)
        {
            try
            {
                var user = await usersRepository.Get(command.Username);

                if (user == null)
                {
                    var message = $"User not found.";
                    logger.ForContext(nameof(command.Username), command.Username)
                        .Information(message);
                    return new FindUserResponse(false, message);
                }
                else
                {
                    var message = $"Successfully retrieved user.";
                    logger.ForContext(nameof(command.Username), command.Username)
                        .Information(message);
                    return new FindUserResponse(true, message);
                }

            }
            catch (Exception ex)
            {
                logger.ForContext(nameof(command.Username), command.Username)
                    .Error(ex, $"Error occurred while retrieving user {command.Username}");
                return new FindUserResponse(false, ex.Message);
            }
        }
    }
}
