﻿// Filename: ClanMemberRemovedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Clans.Dtos;
using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Microsoft.AspNetCore.Identity;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    public sealed class ClanMemberRemovedIntegrationEventHandlerTest : UnitTest
    {
        public ClanMemberRemovedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenClanMemberRemovedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();
            var clanId = new ClanId();
            var user = new User();

            var mockLogger = new MockLogger<ClanMemberRemovedIntegrationEventHandler>();

            TestMock.UserService.Setup(userService => userService.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            TestMock.UserService.Setup(userService => userService.RemoveClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()))
                .ReturnsAsync(new IdentityResult())
                .Verifiable();

            var handler = new ClanMemberRemovedIntegrationEventHandler(TestMock.UserService.Object, mockLogger.Object);

            var integrationEvent = new ClanMemberRemovedIntegrationEvent
            {
                Clan = new ClanDto
                {
                    Id = clanId,
                    Name = "testClan",
                    OwnerId = userId,
                    Summary = null,
                    Members =
                    {
                        new MemberDto
                        {
                            ClanId = clanId,
                            Id = new MemberId(),
                            UserId = userId
                        }
                    }
                },
                UserId = userId
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.UserService.Verify(userService => userService.FindByIdAsync(It.IsAny<string>()), Times.Once);
            TestMock.UserService.Verify(userService => userService.RemoveClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()), Times.Once);
            mockLogger.Verify(Times.Once());
        }
    }
}
