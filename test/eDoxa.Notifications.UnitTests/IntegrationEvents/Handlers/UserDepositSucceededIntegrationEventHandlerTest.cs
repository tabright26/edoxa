﻿// Filename: UserDepositSucceededIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Google.Protobuf.WellKnownTypes;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserDepositSucceededIntegrationEventHandlerTest : UnitTest
    {
        public UserDepositSucceededIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserDepositSucceededIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            TestMock.UserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UserDepositSucceededIntegrationEventHandler(TestMock.UserService.Object, TestMock.SendgridOptions.Object);

            var integrationEvent = new UserDepositSucceededIntegrationEvent
            {
                Transaction = new TransactionDto
                {
                    Currency = new CurrencyDto
                    {
                        Type = EnumCurrencyType.Money,
                        Amount = Money.Fifty.Amount
                    },
                    Description = "test",
                    Id = new TransactionId(),
                    Status = EnumTransactionStatus.Succeeded,
                    Timestamp = DateTime.UtcNow.ToTimestamp(),
                    Type = EnumTransactionType.Deposit
                },
                UserId = new UserId()
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.UserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }
    }
}
