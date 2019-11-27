// Filename: RoleCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using Microsoft.AspNetCore.Identity;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    public sealed class RoleCreatedIntegrationEventHandlerTest : UnitTest
    {
        public RoleCreatedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task RoleCreatedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockRoleManager = new Mock<IRoleManager>();

            mockRoleManager.Setup(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(false).Verifiable();

            mockRoleManager.Setup(roleManager => roleManager.CreateAsync(It.IsAny<Role>())).ReturnsAsync(IdentityResult.Success).Verifiable();

            var handler = new RoleCreatedIntegrationEventHandler(mockRoleManager.Object);

            var integrationEvent = new RoleCreatedIntegrationEvent("role");

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockRoleManager.Verify(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>()), Times.Once);
            mockRoleManager.Verify(roleManager => roleManager.CreateAsync(It.IsAny<Role>()), Times.Once);
        }
    }
}
