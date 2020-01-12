// Filename: ChallengeParticipantRegisteredIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
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
        public ChallengeParticipantRegisteredIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_ChallengeParticipantRegisteredIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var participantId = new ParticipantId();

            var mockAccountService = new Mock<IAccountService>();
            var mockLogger = new MockLogger<ChallengeParticipantRegisteredIntegrationEventHandler>();

            mockAccountService.Setup(accountService => accountService.AccountExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockAccountService.Setup(accountService => accountService.FindAccountAsync(It.IsAny<UserId>())).ReturnsAsync(account).Verifiable();

            mockAccountService
                .Setup(
                    accountService => accountService.MarkAccountTransactionAsSuccededAsync(
                        It.IsAny<IAccount>(),
                        It.IsAny<TransactionMetadata>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            var handler = new ChallengeParticipantRegisteredIntegrationEventHandler(mockAccountService.Object, mockLogger.Object);

            var integrationEvent = new ChallengeParticipantRegisteredIntegrationEvent
            {
                Participant = new ParticipantDto
                {
                    ChallengeId = new ChallengeId(),
                    GamePlayerId = new PlayerId(),
                    UserId = userId,
                    Score = DecimalValue.FromDecimal(20),
                    SynchronizedAt = DateTime.UtcNow.ToTimestamp(),
                    Id = participantId,
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
            mockAccountService.Verify(accountService => accountService.AccountExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.FindAccountAsync(It.IsAny<UserId>()), Times.Once);

            mockAccountService.Verify(
                accountService => accountService.MarkAccountTransactionAsSuccededAsync(
                    It.IsAny<IAccount>(),
                    It.IsAny<TransactionMetadata>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            mockLogger.Verify(Times.Once());
        }
    }
}
