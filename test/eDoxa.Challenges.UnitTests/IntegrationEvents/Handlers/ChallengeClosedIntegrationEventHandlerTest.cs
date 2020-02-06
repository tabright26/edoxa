// Filename: ChallengeClosedIntegrationEventHandlerTest.cs
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
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Challenges.UnitTests.IntegrationEvents.Handlers
{
    public sealed class ChallengeClosedIntegrationEventHandlerTest : UnitTest
    {
        public ChallengeClosedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenChallengeClosedIntegrationEventIsValid_ShouldBeCompletedTask()
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

            var mockLogger = new MockLogger<ChallengeClosedIntegrationEventHandler>();

            TestMock.ChallengeService.Setup(challengeService => challengeService.ChallengeExistsAsync(It.IsAny<ChallengeId>())).ReturnsAsync(true).Verifiable();

            TestMock.ChallengeService.Setup(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challenge)
                .Verifiable();

            TestMock.ChallengeService
                .Setup(
                    challengeService => challengeService.CloseChallengeAsync(
                        It.IsAny<Challenge>(),
                        It.IsAny<IDateTimeProvider>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DomainValidationResult<IChallenge>())
                .Verifiable();

            var handler = new ChallengeClosedIntegrationEventHandler(TestMock.ChallengeService.Object, mockLogger.Object);

            var integrationEvent = new ChallengeClosedIntegrationEvent
            {
                ChallengeId = new ChallengeId(),
                PayoutPrizes =
                {
                    {
                        "test1", new CurrencyDto
                        {
                            Amount = 50.0m,
                            Type = EnumCurrencyType.Money
                        }
                    }
                }
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.ChallengeService.Verify(challengeService => challengeService.ChallengeExistsAsync(It.IsAny<ChallengeId>()), Times.Once);
            TestMock.ChallengeService.Verify(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            TestMock.ChallengeService.Verify(
                challengeService => challengeService.CloseChallengeAsync(It.IsAny<Challenge>(), It.IsAny<IDateTimeProvider>(), It.IsAny<CancellationToken>()),
                Times.Once);

            mockLogger.Verify(Times.Once());
        }
    }
}
