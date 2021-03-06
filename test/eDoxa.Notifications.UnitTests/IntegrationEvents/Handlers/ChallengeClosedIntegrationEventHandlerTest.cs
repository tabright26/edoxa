﻿// Filename: ChallengeClosedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class ChallengeClosedIntegrationEventHandlerTest : UnitTest
    {
        public ChallengeClosedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenChallengeClosedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            TestMock.UserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new ChallengeClosedIntegrationEventHandler(TestMock.UserService.Object, TestMock.SendgridOptions.Object);

            var integrationEvent = new ChallengeClosedIntegrationEvent
            {
                ChallengeId = new ChallengeId(),
                PayoutPrizes =
                {
                    {
                        new UserId().ToString(), new CurrencyDto
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
            TestMock.UserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }
    }
}
