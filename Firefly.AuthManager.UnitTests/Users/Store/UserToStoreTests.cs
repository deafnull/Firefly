using Firefly.AuthManager.Users.Security;
using Firefly.AuthManager.Users.Store;
using Moq;
using Shouldly;
using System;
using Xunit;

namespace Firefly.AuthManager.UnitTests.Users.Store
{
    public class UserToStoreTests
    {
        [Fact]
        public void GivenInvalidSetupThrowsException()
        {
            var request = Mock.Of<IStoreUserRequest>();
            Mock.Get(request).Setup(r => r.Username).Returns(() => Guid.NewGuid().ToString());
            Mock.Get(request).Setup(r => r.Password).Returns(() => Guid.NewGuid().ToString());
            var hashedPassword = new HashedPassword(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            var command = new StoreUserCommand(request);
            
            ShouldThrowExtensions.ShouldThrow(() => new UserToStore(null, hashedPassword), typeof(ArgumentNullException));
            ShouldThrowExtensions.ShouldThrow(() => new UserToStore(command, null), typeof(ArgumentNullException));
            ShouldThrowExtensions.ShouldNotThrow(() => new UserToStore(command, hashedPassword));
        }
    }
}
