// Filename: CreateChallengePayoutFailedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.IntegrationEvents.Handlers;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Challenges.UnitTests.IntegrationEvents.Handlers
{
    public sealed class CreateChallengePayoutFailedIntegrationEventHandlerTest : UnitTest
    {
        public CreateChallengePayoutFailedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) :
            base(testData, testMapper, testValidator)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenCreateChallengePayoutFailedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var challengeId = new ChallengeId();

            var scoring = new Scoring
            {
                {new StatName(Game.LeagueOfLegends), new StatWeighting(50.0f)}
            };

            var challenge = new Challenge(
                challengeId,
                new ChallengeName("test"),
                Game.LeagueOfLegends,
                BestOf.Five,
                Entries.Four,
                new ChallengeTimeline(new DateTimeProvider(DateTime.Now.AddDays(-1)), ChallengeDuration.OneDay),
                scoring);

            var mockLogger = new MockLogger<CreateChallengePayoutFailedIntegrationEventHandler>();

            TestMock.ChallengeService.Setup(challengeService => challengeService.ChallengeExistsAsync(It.IsAny<ChallengeId>())).ReturnsAsync(true).Verifiable();

            TestMock.ChallengeService.Setup(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challenge)
                .Verifiable();

            TestMock.ChallengeService.Setup(challengeService => challengeService.DeleteChallengeAsync(It.IsAny<Challenge>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DomainValidationResult<IChallenge>())
                .Verifiable();

            var handler = new CreateChallengePayoutFailedIntegrationEventHandler(TestMock.ChallengeService.Object, mockLogger.Object);

            var integrationEvent = new CreateChallengePayoutFailedIntegrationEvent
            {
                ChallengeId = challengeId
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.ChallengeService.Verify(challengeService => challengeService.ChallengeExistsAsync(It.IsAny<ChallengeId>()), Times.Once);
            TestMock.ChallengeService.Verify(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            TestMock.ChallengeService.Verify(
                challengeService => challengeService.DeleteChallengeAsync(It.IsAny<Challenge>(), It.IsAny<CancellationToken>()),
                Times.Once);

            mockLogger.Verify(Times.Once());
        }
    }
}
