using Firefly.AuthManager.Users.Store;
using Moq;
using Shouldly;
using System;
using Xunit;

namespace Firefly.AuthManager.UnitTests.Users.Store
{
    public class StoreUserCommandTests
    {
        [Fact]
        public void GivenInvalidSetupThrowsException()
        {
            var request = Mock.Of<IStoreUserRequest>();
            Mock.Get(request).Setup(r => r.Username).Returns(() => Guid.NewGuid().ToString());
            Mock.Get(request).Setup(r => r.Password).Returns(() => Guid.NewGuid().ToString());

            ShouldThrowExtensions.ShouldThrow(() => new StoreUserCommand(null), typeof(ArgumentNullException));
            ShouldThrowExtensions.ShouldNotThrow(() => new StoreUserCommand(request));
        }
    }
}
