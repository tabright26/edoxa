﻿// Filename: UserWithdrawFailedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Google.Protobuf.WellKnownTypes;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserWithdrawFailedIntegrationEventHandlerTest : UnitTest
    {
        public UserWithdrawFailedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserWithdrawFailedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId, new List<ITransaction>());

            var mockLogger = new MockLogger<UserWithdrawFailedIntegrationEventHandler>();

            TestMock.AccountService.Setup(accountRepository => accountRepository.AccountExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            TestMock.AccountService.Setup(accountRepository => accountRepository.FindAccountAsync(It.IsAny<UserId>())).ReturnsAsync(account).Verifiable();

            TestMock.AccountService
                .Setup(
                    accountService => accountService.MarkAccountTransactionAsFailedAsync(
                        It.IsAny<IAccount>(),
                        It.IsAny<TransactionId>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DomainValidationResult<ITransaction>())
                .Verifiable();

            var handler = new UserWithdrawFailedIntegrationEventHandler(TestMock.AccountService.Object, mockLogger.Object);

            var integrationEvent = new UserWithdrawFailedIntegrationEvent
            {
                UserId = userId,
                Transaction = new TransactionDto
                {
                    Id = new TransactionId(),
                    Description = "test",
                    Status = EnumTransactionStatus.Failed,
                    Currency = new CurrencyDto
                    {
                        Type = EnumCurrencyType.Money,
                        Amount = Money.Fifty.Amount
                    },
                    Timestamp = Timestamp.FromDateTime(DateTime.UtcNow),
                    Type = EnumTransactionType.Withdraw
                }
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.AccountService.Verify(accountRepository => accountRepository.AccountExistsAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.AccountService.Verify(accountRepository => accountRepository.FindAccountAsync(It.IsAny<UserId>()), Times.Once);

            TestMock.AccountService.Verify(
                accountService =>
                    accountService.MarkAccountTransactionAsFailedAsync(It.IsAny<IAccount>(), It.IsAny<TransactionId>(), It.IsAny<CancellationToken>()),
                Times.Once);

            mockLogger.Verify(Times.Once());
        }
    }
}
