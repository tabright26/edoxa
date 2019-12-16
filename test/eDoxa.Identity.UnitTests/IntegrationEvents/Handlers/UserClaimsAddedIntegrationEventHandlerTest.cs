﻿// Filename: UserClaimsAddedIntegrationEventHandlerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using Microsoft.AspNetCore.Identity;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserClaimsAddedIntegrationEventHandlerTest : UnitTest
    {
        public UserClaimsAddedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task UserClaimsAddedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(roleManager => roleManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()).Verifiable();

            mockUserManager.Setup(roleManager => roleManager.GetClaimsAsync(It.IsAny<User>())).ReturnsAsync(new List<Claim>()).Verifiable();

            mockUserManager.Setup(roleManager => roleManager.AddClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var handler = new UserClaimsAddedIntegrationEventHandler(mockUserManager.Object);

            var integrationEvent = new UserClaimsAddedIntegrationEvent
            {
                UserId = Guid.NewGuid().ToString(),
                Claims =
                {
                    new UserClaimDto
                    {
                        Type = "role",
                        Value = "admin"
                    }
                }
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserManager.Verify(roleManager => roleManager.FindByIdAsync(It.IsAny<string>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.GetClaimsAsync(It.IsAny<User>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.AddClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()), Times.Once);
        }
    }
}
