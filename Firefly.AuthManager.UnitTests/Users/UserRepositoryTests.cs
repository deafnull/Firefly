using Firefly.AuthManager.Users;
using Moq;
using Shouldly;
using System;
using Xunit;

namespace Firefly.AuthManager.UnitTests.Users
{
    public class PostedUsersRepositoryTests
    {
        [Fact]
        public void GivenInvalidSetupThrowsException()
        {
            ShouldThrowExtensions.ShouldThrow(() => new UsersRepository(null), typeof(ArgumentNullException));
            ShouldThrowExtensions.ShouldNotThrow(() => new UsersRepository(Mock.Of<IFireflyAuthDatabase>()));
        }
    }
}
