// Filename: UserClaimsAddedIntegrationEventHandlerTest.cs
// Date Created: 2019-08-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
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
    public sealed class UserClaimsAddedIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task UserClaimsAddedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(roleManager => roleManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()).Verifiable();

            mockUserManager.Setup(roleManager => roleManager.GetClaimsAsync(It.IsAny<User>()))
                .ReturnsAsync(
                    new List<Claim>()
                )
                .Verifiable();

            mockUserManager.Setup(roleManager => roleManager.AddClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var handler = new UserClaimsAddedIntegrationEventHandler(mockUserManager.Object);

            var integrationEvent = new UserClaimsAddedIntegrationEvent(
                Guid.NewGuid(),
                new Dictionary<string, string>
                {
                    ["role"] = "admin"
                }
            );

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserManager.Verify(roleManager => roleManager.FindByIdAsync(It.IsAny<string>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.GetClaimsAsync(It.IsAny<User>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.AddClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()), Times.Once);
        }
    }
}
