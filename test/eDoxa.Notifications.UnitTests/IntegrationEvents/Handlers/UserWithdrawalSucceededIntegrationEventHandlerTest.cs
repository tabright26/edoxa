// Filename: UserWithdrawalSucceededIntegrationEventHandlerTest.cs
// Date Created: 2019-12-17
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Google.Protobuf.WellKnownTypes;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserWithdrawalSucceededIntegrationEventHandlerTest : UnitTest
    {
        public UserWithdrawalSucceededIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserWithdrawalSucceededIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            mockUserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UserWithdrawalSucceededIntegrationEventHandler(mockUserService.Object);

            var integrationEvent = new UserWithdrawalSucceededIntegrationEvent
            {
                Transaction = new TransactionDto()
                {
                    Amount = new DecimalValue(50.0m),
                    Currency = CurrencyDto.Money,
                    Description = "test",
                    Id = new TransactionId(),
                    Status = TransactionStatusDto.Succeded,
                    Timestamp = DateTime.UtcNow.ToTimestamp(),
                    Type = TransactionTypeDto.Withdrawal
                },
                UserId = new UserId()
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
