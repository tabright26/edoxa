// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
    public sealed class RoleCreatedIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task RoleCreatedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockRoleManager = new Mock<IRoleManager>();

            mockRoleManager
                .Setup(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false)
                .Verifiable();

            mockRoleManager.Setup(roleManager =>
                    roleManager.CreateAsync(It.IsAny<Role>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var handler = new RoleCreatedIntegrationEventHandler(
                mockRoleManager.Object);

            var integrationEvent = new RoleCreatedIntegrationEvent("role");

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockRoleManager.Verify(roleManager => roleManager.RoleExistsAsync(It.IsAny<string>()), Times.Once);
            mockRoleManager.Verify(roleManager => roleManager.CreateAsync(It.IsAny<Role>()), Times.Once);
        }
    }
}
