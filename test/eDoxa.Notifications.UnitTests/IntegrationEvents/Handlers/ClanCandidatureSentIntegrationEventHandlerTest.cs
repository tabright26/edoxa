// Filename: ClanCandidatureSentIntegrationEventHandlerTest.cs
// Date Created: 2019-12-18
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Clans.Dtos;
using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class ClanCandidatureSentIntegrationEventHandlerTest : UnitTest
    {
        public ClanCandidatureSentIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenClanCandidatureSentIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();
            var clanId = new ClanId();

            var mockUserService = new Mock<IUserService>();

            mockUserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new ClanCandidatureSentIntegrationEventHandler(mockUserService.Object);

            var integrationEvent = new ClanCandidatureSentIntegrationEvent
            {
                Clan = new ClanDto
                {
                    Id = clanId,
                    Name = "testClan",
                    OwnerId = userId,
                    Summary = "",
                    Members =
                    {
                        new MemberDto
                        {
                            ClanId = clanId,
                            Id = new MemberId(),
                            UserId = userId
                        }
                    }
                }
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
