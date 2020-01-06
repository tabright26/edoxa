// Filename: UserDepositFailedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-17
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Google.Protobuf.WellKnownTypes;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserDepositFailedIntegrationEventHandlerTest : UnitTest
    {
        public UserDepositFailedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserDepositFailedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            mockUserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UserDepositFailedIntegrationEventHandler(mockUserService.Object);

            var integrationEvent = new UserDepositFailedIntegrationEvent
            {
                Transaction = new TransactionDto()
                {
                    Amount = new DecimalValue(50.0m),
                    Currency = CurrencyDto.Money,
                    Description = "test",
                    Id = new TransactionId(),
                    Status = TransactionStatusDto.Failed,
                    Timestamp = DateTime.UtcNow.ToTimestamp(),
                    Type = TransactionTypeDto.Deposit
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
