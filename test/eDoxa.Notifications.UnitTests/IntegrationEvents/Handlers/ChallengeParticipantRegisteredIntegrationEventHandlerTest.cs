// Filename: ChallengeParticipantRegisteredIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Google.Protobuf.WellKnownTypes;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class ChallengeParticipantRegisteredIntegrationEventHandlerTest : UnitTest
    {
        public ChallengeParticipantRegisteredIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenChallengeParticipantRegisteredIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var participantId = new ParticipantId();

            TestMock.UserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new ChallengeParticipantRegisteredIntegrationEventHandler(TestMock.UserService.Object);

            var integrationEvent = new ChallengeParticipantRegisteredIntegrationEvent
            {
                Participant = new ParticipantDto
                {
                    ChallengeId = new ChallengeId(),
                    GamePlayerId = "testId",
                    Id = participantId,
                    Score = new DecimalValue(50.0m),
                    SynchronizedAt = DateTime.UtcNow.ToTimestamp(),
                    UserId = new UserId(),
                    Matches =
                    {
                        new MatchDto
                        {
                            Id = new MatchId(),
                            ParticipantId = participantId,
                            Score = DecimalValue.FromDecimal(10)
                        },
                        new MatchDto
                        {
                            Id = new MatchId(),
                            ParticipantId = participantId,
                            Score = DecimalValue.FromDecimal(10)
                        }
                    }
                }
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.UserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }
    }
}
