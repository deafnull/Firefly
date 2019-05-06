using Firefly.AuthManager.Api.Users;
using Firefly.AuthManager.Users.Find;
using Firefly.AuthManager.Users.Store;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Firefly.AuthManager.UnitTests.Api
{
    public class UsersControllerTests
    {
        private readonly IMediator mediator = Mock.Of<IMediator>();
        private readonly UsersController controller;

        public UsersControllerTests()
        {
            controller = new UsersController(mediator);
        }

        [Fact]
        public async Task CanFind()
        {
            var findUserResponse = Mock.Of<IFindUserResponse>();
            Mock.Get(mediator)
                .Setup(m => m.Send(It.IsAny<FindUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => findUserResponse);
            Mock.Get(findUserResponse)
                .SetupGet(m => m.Success)
                .Returns(() => true);

            var result = await controller.Find(Guid.NewGuid().ToString());

            var okObjectResult = result.ShouldBeAssignableTo<OkObjectResult>();
            okObjectResult.Value.ShouldBeAssignableTo<IFindUserResponse>();
            Mock.Get(mediator)
                .Verify(m => m.Send(It.IsAny<FindUserQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CanPost()
        {
            Mock.Get(mediator)
                .Setup(m => m.Send(It.IsAny<StoreUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => Mock.Of<IStoreUserResponse>());

            var result = await controller.Store(new StoreUserRequest() { Username = Guid.NewGuid().ToString(), Password = Guid.NewGuid().ToString() });

            var okObjectResult = result.ShouldBeAssignableTo<OkObjectResult>();
            okObjectResult.Value.ShouldBeAssignableTo<IStoreUserResponse>();
            Mock.Get(mediator)
                .Verify(m => m.Send(It.IsAny<StoreUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
