﻿// Filename: UserClaimsReplacedIntegrationEventHandlerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Identity;

using Moq;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserClaimsReplacedIntegrationEventHandlerTest : UnitTest
    {
        public UserClaimsReplacedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

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
                new UserId(),
                1,
                new Claims(new Seedwork.Security.Claim("role", "admin")),
                new Claims(new Seedwork.Security.Claim("role", "user")));

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserManager.Verify(roleManager => roleManager.FindByIdAsync(It.IsAny<string>()), Times.Once);
            mockUserManager.Verify(roleManager => roleManager.ReplaceClaimAsync(It.IsAny<User>(), It.IsAny<Claim>(), It.IsAny<Claim>()), Times.Once);
        }
    }
}
