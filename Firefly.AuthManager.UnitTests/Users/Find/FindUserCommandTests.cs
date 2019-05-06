using Firefly.AuthManager.Users.Find;
using Shouldly;
using System;
using Xunit;

namespace Firefly.AuthManager.UnitTests.Users.Find
{
    public class FindUserCommandTests
    {
        [Fact]
        public void GivenInvalidSetupThrowsException()
        {
            ShouldThrowExtensions.ShouldThrow(() => new FindUserQuery(null), typeof(ArgumentNullException));
            ShouldThrowExtensions.ShouldNotThrow(() => new FindUserQuery(Guid.Empty.ToString()));
        }
    }
}
