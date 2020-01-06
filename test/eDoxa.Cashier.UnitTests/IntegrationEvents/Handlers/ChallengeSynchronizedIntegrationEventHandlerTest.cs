// Filename: ChallengeSynchronizedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-17
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using CsvHelper;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;
using eDoxa.ServiceBus.Abstractions;

using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    public sealed class ChallengeSynchronizedIntegrationEventHandlerTest : UnitTest
    {
        public ChallengeSynchronizedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_ChallengeSynchronizedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var challengeId = new ChallengeId();

            var factory = new ChallengePayoutFactory();
            var strategy = factory.CreateInstance();
            var payout = strategy.GetPayout(PayoutEntries.Five, MoneyEntryFee.Fifty);

            var challenge = new Challenge(challengeId, MoneyEntryFee.Fifty, payout);

            var mockAccountService = new Mock<IAccountService>();
            var mockChallengeService = new Mock<IChallengeService>();
            var mockServiceBus = new Mock<IServiceBusPublisher>();


            var mockLogger = new MockLogger<ChallengeSynchronizedIntegrationEventHandler>();

            mockChallengeService.Setup(challengeService => challengeService.ChallengeExistsAsync(It.IsAny<ChallengeId>())).ReturnsAsync(true).Verifiable();

            mockChallengeService.Setup(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockAccountService
                .Setup(
                    accountService => accountService.PayoutChallengeAsync(
                        It.IsAny<Scoreboard>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(DomainValidationResult.Succeded(new PayoutPrizes()))
                .Verifiable();

            mockServiceBus.Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<ChallengeClosedIntegrationEvent>())).Returns(Task.CompletedTask).Verifiable();


            var handler = new ChallengeSynchronizedIntegrationEventHandler(mockChallengeService.Object, mockAccountService.Object, mockServiceBus.Object, mockLogger.Object);

            var integrationEvent = new ChallengeSynchronizedIntegrationEvent
            {
                ChallengeId = new ChallengeId()
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockChallengeService.Verify(challengeService => challengeService.ChallengeExistsAsync(It.IsAny<ChallengeId>()), Times.Once);
            mockChallengeService.Verify(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockAccountService.Verify(
                accountService => accountService.PayoutChallengeAsync(
                    It.IsAny<Scoreboard>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            mockServiceBus.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<ChallengeClosedIntegrationEvent>()), Times.Once);

            mockLogger.Verify(Times.Once());
        }
    }
}
