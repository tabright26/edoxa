// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandlerTest : UnitTest
    {

        [Fact]
        public async Task HandleAsync_WhenUserCreatedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();

            var mockLogger = new MockLogger<UserCreatedIntegrationEventHandler>();

            mockAccountService.Setup(accountRepository => accountRepository.AccountExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            mockAccountService.Setup(accountRepository => accountRepository.CreateAccountAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(mockAccountService.Object, mockLogger.Object);

            var integrationEvent = new UserCreatedIntegrationEvent
            {
                UserId = Guid.NewGuid().ToString(),
                Email = new EmailDto
                {
                    Address = "noreply@edoxa.gg"
                },
                CountryIsoCode = EnumCountryIsoCode.CA
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockAccountService.Verify(accountRepository => accountRepository.AccountExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountRepository => accountRepository.CreateAccountAsync(It.IsAny<UserId>()), Times.Once);
        }

        public UserCreatedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }
    }
}
