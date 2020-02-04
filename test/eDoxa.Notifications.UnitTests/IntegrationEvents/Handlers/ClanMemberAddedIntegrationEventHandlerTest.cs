// Filename: ClanMemberAddedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Clans.Dtos;
using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class ClanMemberAddedIntegrationEventHandlerTest : UnitTest
    {
        public ClanMemberAddedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenClanMemberAddedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();
            var clanId = new ClanId();

            TestMock.UserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new ClanMemberAddedIntegrationEventHandler(TestMock.UserService.Object);

            var integrationEvent = new ClanMemberAddedIntegrationEvent
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
                },
                UserId = new UserId()
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.UserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }
    }
}
