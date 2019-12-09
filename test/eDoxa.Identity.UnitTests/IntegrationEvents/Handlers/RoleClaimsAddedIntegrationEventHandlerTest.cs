// Filename: RoleClaimAddedIntegrationEventHandlerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Identity;

using Moq;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    public sealed class RoleClaimsAddedIntegrationEventHandlerTest : UnitTest
    {
        public RoleClaimsAddedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task RoleClaimAddedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockRoleManager = new Mock<IRoleService>();

            mockRoleManager.Setup(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true).Verifiable();

            mockRoleManager.Setup(roleManager => roleManager.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new Role()).Verifiable();

            mockRoleManager.Setup(roleManager => roleManager.AddClaimAsync(It.IsAny<Role>(), It.IsAny<Claim>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var handler = new RoleClaimsAddedIntegrationEventHandler(mockRoleManager.Object);

            var integrationEvent = new RoleClaimsAddedIntegrationEvent("role", new Claims(new Seedwork.Security.Claim( "admin", "allow")));

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockRoleManager.Verify(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>()), Times.Once);
            mockRoleManager.Verify(roleManager => roleManager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            mockRoleManager.Verify(roleManager => roleManager.AddClaimAsync(It.IsAny<Role>(), It.IsAny<Claim>()), Times.Once);
        }
    }
}
