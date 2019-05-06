using Firefly.AuthManager.Users;
using Firefly.AuthManager.Users.Store;
using Moq;
using Shouldly;
using System;
using Xunit;

namespace Firefly.AuthManager.UnitTests.Users.Store
{
    public class StoreUserResponseTests
    {
        [Fact]
        public void GivenInvalidSetupThrowsException()
        {
            ShouldThrowExtensions.ShouldThrow(() => new StoreUserResponse(true, null), typeof(ArgumentNullException));
            ShouldThrowExtensions.ShouldNotThrow(() => new StoreUserResponse(true, "message"));
        }
    }
}