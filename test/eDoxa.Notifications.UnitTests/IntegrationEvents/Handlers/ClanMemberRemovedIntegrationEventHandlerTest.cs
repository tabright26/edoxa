// Filename: ClanMemberRemovedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-17
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
    public sealed class ClanMemberRemovedIntegrationEventHandlerTest : UnitTest
    {
        public ClanMemberRemovedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenClanMemberRemovedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();
            var clanId = new ClanId();

            var mockUserService = new Mock<IUserService>();

            mockUserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new ClanMemberRemovedIntegrationEventHandler(mockUserService.Object);

            var integrationEvent = new ClanMemberRemovedIntegrationEvent
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
                UserId = userId
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
