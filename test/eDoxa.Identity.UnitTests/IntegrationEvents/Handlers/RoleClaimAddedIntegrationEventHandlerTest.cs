// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;

using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class RoleClaimAddedIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task RoleClaimAddedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockRoleManager = new Mock<IRoleManager>();

            mockRoleManager
                .Setup(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockRoleManager.Setup(roleManager =>
                    roleManager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Role{})
                .Verifiable();

            mockRoleManager.Setup(roleManager =>
                    roleManager.AddClaimAsync(It.IsAny<Role>(), It.IsAny<Claim>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var handler = new RoleClaimAddedIntegrationEventHandler(
                mockRoleManager.Object);

            var integrationEvent = new RoleClaimAddedIntegrationEvent("role", "admin", "allow");

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockRoleManager.Verify(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>()), Times.Once);
            mockRoleManager.Verify(roleManager => roleManager.FindByNameAsync(It.IsAny<string>()), Times.Once);
            mockRoleManager.Verify(roleManager => roleManager.AddClaimAsync(It.IsAny<Role>(), It.IsAny<Claim>()), Times.Once);
        }
    }
}
