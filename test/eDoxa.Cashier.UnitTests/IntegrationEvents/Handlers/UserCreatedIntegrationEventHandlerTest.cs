// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
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
        public UserCreatedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserCreatedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockLogger = new MockLogger<UserCreatedIntegrationEventHandler>();

            TestMock.AccountService.Setup(accountRepository => accountRepository.AccountExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            TestMock.AccountService.Setup(accountRepository => accountRepository.CreateAccountAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<IAccount>())
                .Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(TestMock.CashierAppSettingsOptions.Object, TestMock.AccountService.Object, mockLogger.Object);

            var integrationEvent = new UserCreatedIntegrationEvent
            {
                UserId = new UserId(),
                Email = new EmailDto
                {
                    Address = "noreply@edoxa.gg"
                },
                Country = EnumCountryIsoCode.CA,
                Ip = "10.10.10.10"
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.AccountService.Verify(accountRepository => accountRepository.AccountExistsAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.AccountService.Verify(accountRepository => accountRepository.CreateAccountAsync(It.IsAny<UserId>()), Times.Once);
        }
    }
}
