// Filename: UserRoleAddedIntegrationEventHandlerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Identity;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserRoleAddedIntegrationEventHandlerTest : UnitTest
    {
        public UserRoleAddedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task UserRoleAddedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(roleManager => roleManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()).Verifiable();

            mockUserManager.Setup(roleManager => roleManager.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false).Verifiable();

            mockUserManager.Setup(roleManager => roleManager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var handler = new UserRoleAddedIntegrationEventHandler(mockUserManager.Object);

            var integrationEvent = new UserRoleAddedIntegrationEvent(new UserId(), "role");

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserManager.Verify(roleManager => roleManager.FindByIdAsync(It.IsAny<string>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
