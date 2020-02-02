// Filename: ClanMemberAddedIntegrationEventHandlerTest.cs
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
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Microsoft.AspNetCore.Identity;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.IntegrationEvents.Handlers
{
    public sealed class ClanMemberAddedIntegrationEventHandlerTest : UnitTest
    {
        public ClanMemberAddedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenClanMemberAddedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();
            var clanId = new ClanId();

            var user = new User();

            var mockUserService = new Mock<IUserService>();
            var mockLogger = new MockLogger<ClanMemberAddedIntegrationEventHandler>();

            mockUserService.Setup(userService => userService.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            mockUserService.Setup(userService => userService.AddClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()))
                .ReturnsAsync(new IdentityResult())
                .Verifiable();

            var handler = new ClanMemberAddedIntegrationEventHandler(mockUserService.Object, mockLogger.Object);

            var integrationEvent = new ClanMemberAddedIntegrationEvent
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
                UserId = new UserId()
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserService.Verify(userService => userService.FindByIdAsync(It.IsAny<string>()), Times.Once);
            mockUserService.Verify(userService => userService.AddClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()), Times.Once);
            mockLogger.Verify(Times.Once());
        }
    }
}
