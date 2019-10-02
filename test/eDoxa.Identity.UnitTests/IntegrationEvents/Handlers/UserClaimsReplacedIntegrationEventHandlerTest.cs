﻿// Filename: UserClaimsReplacedIntegrationEventHandlerTest.cs
// Date Created: 2019-09-16
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

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserClaimsReplacedIntegrationEventHandlerTest
    {
        [Fact]
        public async Task UserClaimsReplacedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(roleManager => roleManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()).Verifiable();

            mockUserManager.Setup(roleManager => roleManager.ReplaceClaimAsync(It.IsAny<User>(), It.IsAny<Claim>(), It.IsAny<Claim>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var handler = new UserClaimsReplacedIntegrationEventHandler(mockUserManager.Object);

            var integrationEvent = new UserClaimsReplacedIntegrationEvent(
                Guid.NewGuid(),
                1,
                new Dictionary<string, string>
                {
                    ["role"] = "admin"
                },
                new Dictionary<string, string>
                {
                    ["role"] = "user"
                });

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserManager.Verify(roleManager => roleManager.FindByIdAsync(It.IsAny<string>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.ReplaceClaimAsync(It.IsAny<User>(), It.IsAny<Claim>(), It.IsAny<Claim>()), Times.Once);
        }
    }
}
