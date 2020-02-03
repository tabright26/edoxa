// Filename: RegisterChallengeParticipantFailedIntegrationEventHandlerTest.cs
// Date Created: 2020-01-17
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
    public sealed class RegisterChallengeParticipantFailedIntegrationEventHandlerTest : UnitTest
    {
        public RegisterChallengeParticipantFailedIntegrationEventHandlerTest(
            TestDataFixture testData,
            TestMapperFixture testMapper,
            TestValidator testValidator
        ) : base(testData, testMapper, testValidator)
        {
        }

        [Fact]
        public async Task HandleAsync_RegisterChallengeParticipantFailedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var parcipipantId = new ParticipantId();

            var mockLogger = new MockLogger<RegisterChallengeParticipantFailedIntegrationEventHandler>();

            TestMock.AccountService.Setup(accountService => accountService.AccountExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            TestMock.AccountService.Setup(accountService => accountService.FindAccountAsync(It.IsAny<UserId>())).ReturnsAsync(account).Verifiable();

            TestMock.AccountService
                .Setup(
                    accountService => accountService.MarkAccountTransactionAsCanceledAsync(
                        It.IsAny<IAccount>(),
                        It.IsAny<TransactionMetadata>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DomainValidationResult<ITransaction>())
                .Verifiable();

            var handler = new RegisterChallengeParticipantFailedIntegrationEventHandler(TestMock.AccountService.Object, mockLogger.Object);

            var integrationEvent = new RegisterChallengeParticipantFailedIntegrationEvent
            {
                Participant = new ParticipantDto
                {
                    ChallengeId = new ChallengeId(),
                    GamePlayerId = new PlayerId(),
                    UserId = userId,
                    Score = DecimalValue.FromDecimal(20),
                    SynchronizedAt = DateTime.UtcNow.ToTimestamp(),
                    Id = parcipipantId,
                    Matches =
                    {
                        new MatchDto
                        {
                            Id = new MatchId(),
                            ParticipantId = parcipipantId,
                            Score = DecimalValue.FromDecimal(10)
                        },
                        new MatchDto
                        {
                            Id = new MatchId(),
                            ParticipantId = parcipipantId,
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
                accountService => accountService.MarkAccountTransactionAsCanceledAsync(
                    It.IsAny<IAccount>(),
                    It.IsAny<TransactionMetadata>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            mockLogger.Verify(Times.Once());
        }
    }
}
