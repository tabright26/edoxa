﻿// Filename: UserRoleRemovedIntegrationEventHandlerTest.cs
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
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Identity;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserRoleRemovedIntegrationEventHandlerTest : UnitTest
    {
        public UserRoleRemovedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task UserRoleRemovedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(roleManager => roleManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()).Verifiable();

            mockUserManager.Setup(roleManager => roleManager.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true).Verifiable();

            mockUserManager.Setup(roleManager => roleManager.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var handler = new UserRoleRemovedIntegrationEventHandler(mockUserManager.Object);

            var integrationEvent = new UserRoleRemovedIntegrationEvent(new UserId(), "role");

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserManager.Verify(roleManager => roleManager.FindByIdAsync(It.IsAny<string>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
