// Filename: UserClaimsRemovedIntegrationEventHandlerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using Microsoft.AspNetCore.Identity;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserClaimsRemovedIntegrationEventHandlerTest : UnitTest
    {
        public UserClaimsRemovedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task UserClaimsRemovedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(roleManager => roleManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()).Verifiable();

            mockUserManager.Setup(roleManager => roleManager.RemoveClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var handler = new UserClaimsRemovedIntegrationEventHandler(mockUserManager.Object);

            var integrationEvent = new UserClaimsRemovedIntegrationEvent
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
            mockUserManager.Verify(roleManager => roleManager.RemoveClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()), Times.Once);
        }
    }
}
