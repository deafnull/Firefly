using Firefly.AuthManager.Users;
using Firefly.AuthManager.Users.Security;
using Firefly.AuthManager.Users.Store;
using Moq;
using Serilog;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Firefly.AuthManager.UnitTests.Users.Store
{
    public class StoreUserHandlerTests
    {
        private readonly IUsersRepository repository = Mock.Of<IUsersRepository>();
        private readonly ILogger logger = Mock.Of<ILogger>();
        private readonly IPasswordHasher passwordHasher = Mock.Of<IPasswordHasher>();
        private readonly IUsernamePasswordPolicyValidator usernamePasswordPolicyValidator = Mock.Of<IUsernamePasswordPolicyValidator>();
        private readonly StoreUserHandler handler;

        public StoreUserHandlerTests()
        {
            Mock.Get(logger).Setup(l => l.ForContext<StoreUserHandler>())
                .Returns(logger);
            Mock.Get(logger).Setup(l => l.ForContext(It.IsAny<string>(), It.IsAny<string>(), false))
                .Returns(logger);
            Mock.Get(passwordHasher).Setup(l => l.Hash(It.IsAny<string>()))
                .Returns(new HashedPassword(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

            handler = new StoreUserHandler(repository, usernamePasswordPolicyValidator, passwordHasher, logger);
        }

        [Fact]
        public void GivenInvalidSetupThrowsException()
        {
            ShouldThrowExtensions.ShouldThrow(() => new StoreUserHandler(repository, null, null, null), typeof(ArgumentNullException));
            ShouldThrowExtensions.ShouldThrow(() => new StoreUserHandler(null, usernamePasswordPolicyValidator, null, null), typeof(ArgumentNullException));
            ShouldThrowExtensions.ShouldThrow(() => new StoreUserHandler(null, null, passwordHasher, null), typeof(ArgumentNullException));
            ShouldThrowExtensions.ShouldThrow(() => new StoreUserHandler(null, null, null, logger), typeof(ArgumentNullException));
            ShouldThrowExtensions.ShouldNotThrow(() => new StoreUserHandler(repository, usernamePasswordPolicyValidator, passwordHasher, logger));
        }

        [Fact]
        public async Task GivenSuccessReturnsResponse()
        {
            var request = Mock.Of<IStoreUserRequest>();
            Mock.Get(request).Setup(r => r.Username).Returns(() => Guid.NewGuid().ToString());
            Mock.Get(request).Setup(r => r.Password).Returns(() => Guid.NewGuid().ToString());
            var command = new StoreUserCommand(request);

            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldNotBeNull();
            Mock.Get(logger).Verify(l => l.Information($"Successfully stored user."), Times.Once);
        }

        [Fact]
        public async Task GivenErrorOccursLogsUser()
        {
            Mock.Get(repository).Setup(l => l.Insert(It.IsAny<IUser>()))
                .Returns(() => throw new Exception());
            var request = Mock.Of<IStoreUserRequest>();
            Mock.Get(request).Setup(r => r.Username).Returns(() => Guid.NewGuid().ToString());
            Mock.Get(request).Setup(r => r.Password).Returns(() => Guid.NewGuid().ToString());
            var command = new StoreUserCommand(request);

            await handler.Handle(command, CancellationToken.None);

            Mock.Get(logger).Verify(l => l.Error(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
        }
    }
}
