﻿// Filename: ChallengeParticipantRegisteredIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Google.Protobuf.WellKnownTypes;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    public sealed class ChallengeParticipantRegisteredIntegrationEventHandlerTest : UnitTest
    {
        public ChallengeParticipantRegisteredIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) :
            base(testData, testMapper, testValidator)
        {
        }

        [Fact]
        public async Task HandleAsync_ChallengeParticipantRegisteredIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var participantId = new ParticipantId();

            var mockLogger = new MockLogger<ChallengeParticipantRegisteredIntegrationEventHandler>();

            TestMock.AccountService.Setup(accountService => accountService.AccountExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            TestMock.AccountService.Setup(accountService => accountService.FindAccountAsync(It.IsAny<UserId>())).ReturnsAsync(account).Verifiable();

            TestMock.AccountService
                .Setup(
                    accountService => accountService.MarkAccountTransactionAsSucceededAsync(
                        It.IsAny<IAccount>(),
                        It.IsAny<TransactionMetadata>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DomainValidationResult<ITransaction>())
                .Verifiable();

            var handler = new ChallengeParticipantRegisteredIntegrationEventHandler(TestMock.AccountService.Object, mockLogger.Object);

            var integrationEvent = new ChallengeParticipantRegisteredIntegrationEvent
            {
                Participant = new ChallengeParticipantDto
                {
                    ChallengeId = new ChallengeId(),
                    GamePlayerId = new PlayerId(),
                    UserId = userId,
                    Score = DecimalValue.FromDecimal(20),
                    SynchronizedAt = DateTime.UtcNow.ToTimestamp(),
                    Id = participantId,
                    Matches =
                    {
                        new ChallengeMatchDto
                        {
                            Id = new MatchId(),
                            ParticipantId = participantId,
                            Score = DecimalValue.FromDecimal(10)
                        },
                        new ChallengeMatchDto
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
            TestMock.AccountService.Verify(accountService => accountService.AccountExistsAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.AccountService.Verify(accountService => accountService.FindAccountAsync(It.IsAny<UserId>()), Times.Once);

            TestMock.AccountService.Verify(
                accountService => accountService.MarkAccountTransactionAsSucceededAsync(
                    It.IsAny<IAccount>(),
                    It.IsAny<TransactionMetadata>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            mockLogger.Verify(Times.Once());
        }
    }
}
