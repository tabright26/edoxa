// Filename: ChallengeSynchronizedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    public sealed class ChallengeSynchronizedIntegrationEventHandlerTest : UnitTest
    {
        public ChallengeSynchronizedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task HandleAsync_ChallengeSynchronizedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var challengeId = new ChallengeId();

            var factory = new ChallengePayoutFactory();
            var strategy = factory.CreateInstance();
            var payout = strategy.GetChallengePayout(ChallengePayoutEntries.Five, MoneyEntryFee.Fifty);

            var challenge = new Challenge(challengeId, payout);

            var mockLogger = new MockLogger<ChallengeSynchronizedIntegrationEventHandler>();

            TestMock.ChallengeService.Setup(challengeService => challengeService.ChallengeExistsAsync(It.IsAny<ChallengeId>())).ReturnsAsync(true).Verifiable();

            TestMock.ChallengeService.Setup(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challenge)
                .Verifiable();

            TestMock.ChallengeService
                .Setup(
                    challengeService => challengeService.CloseChallengeAsync(
                        It.IsAny<IChallenge>(),
                        It.IsAny<Dictionary<UserId, decimal?>>(),
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new ChallengeSynchronizedIntegrationEventHandler(TestMock.ChallengeService.Object, mockLogger.Object);

            var integrationEvent = new ChallengeSynchronizedIntegrationEvent
            {
                ChallengeId = new ChallengeId()
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.ChallengeService.Verify(challengeService => challengeService.ChallengeExistsAsync(It.IsAny<ChallengeId>()), Times.Once);

            TestMock.ChallengeService.Verify(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            TestMock.ChallengeService.Verify(
                challengeService => challengeService.CloseChallengeAsync(
                    It.IsAny<IChallenge>(),
                    It.IsAny<Dictionary<UserId, decimal?>>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            mockLogger.Verify(Times.Once());
        }
    }
}
