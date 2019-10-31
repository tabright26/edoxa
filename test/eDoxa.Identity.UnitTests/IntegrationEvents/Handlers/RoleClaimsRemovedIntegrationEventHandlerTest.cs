// Filename: RoleClaimRemovedIntegrationEventHandlerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.Identity.TestHelpers;
using eDoxa.Identity.TestHelpers.Fixtures;
using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Identity;

using Moq;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    public sealed class RoleClaimsRemovedIntegrationEventHandlerTest : UnitTest
    {
        public RoleClaimsRemovedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task RoleClaimRemovedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockRoleManager = new Mock<IRoleManager>();

            mockRoleManager.Setup(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true).Verifiable();

            mockRoleManager.Setup(roleManager => roleManager.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new Role()).Verifiable();

            mockRoleManager.Setup(roleManager => roleManager.RemoveClaimAsync(It.IsAny<Role>(), It.IsAny<Claim>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var handler = new RoleClaimsRemovedIntegrationEventHandler(mockRoleManager.Object);

            var integrationEvent = new RoleClaimsRemovedIntegrationEvent("role", new Claims(new Seedwork.Security.Claim("admin", "allow")));

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockRoleManager.Verify(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>()), Times.Once);
            mockRoleManager.Verify(roleManager => roleManager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            mockRoleManager.Verify(roleManager => roleManager.RemoveClaimAsync(It.IsAny<Role>(), It.IsAny<Claim>()), Times.Once);
        }
    }
}
