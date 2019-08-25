// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.Seedwork.Domain;

using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserRoleAddedIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task UserRoleAddedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager
                .Setup(roleManager => roleManager.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new User())
                .Verifiable();

            mockUserManager.Setup(roleManager =>
                    roleManager.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(false)
                .Verifiable();

            mockUserManager.Setup(roleManager =>
                    roleManager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var handler = new UserRoleAddedIntegrationEventHandler(
                mockUserManager.Object);

            var integrationEvent = new UserRoleAddedIntegrationEvent(Guid.NewGuid(), "role");

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserManager.Verify(roleManager => roleManager.FindByIdAsync(It.IsAny<string>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
