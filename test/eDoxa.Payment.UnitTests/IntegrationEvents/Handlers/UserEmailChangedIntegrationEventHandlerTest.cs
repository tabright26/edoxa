//// Filename: UserEmailChangedIntegrationEventHandlerTest.cs
//// Date Created: 2019-11-25
//// 
//// ================================================
//// Copyright © 2020, eDoxa. All rights reserved.

//using System;
//using System.Threading.Tasks;

//using eDoxa.Grpc.Protos.Identity.Dtos;
//using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
//using eDoxa.Payment.Api.IntegrationEvents.Handlers;
//using eDoxa.Payment.TestHelper;
//using eDoxa.Payment.TestHelper.Fixtures;
//using eDoxa.Seedwork.Domain;
//using eDoxa.Seedwork.Domain.Misc;
//using eDoxa.Seedwork.TestHelper.Mocks;

//using Moq;

//using Stripe;

//using Xunit;

//namespace eDoxa.Payment.UnitTests.IntegrationEvents.Handlers
//{
//    public sealed class UserEmailChangedIntegrationEventHandlerTest : UnitTest
//    {
//        public UserEmailChangedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
//        {
//        }

//        [Fact]
//        public async Task HandleAsync_WhenUserEmailChangedIntegrationEventIsValid_ShouldBeCompletedTask()
//        {
//            // Arrange
//            var mockLogger = new MockLogger<UserEmailChangedIntegrationEventHandler>();

//            TestMock.StripeService.Setup(stripeService => stripeService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

//            TestMock.StripeAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("ConnectAccountId").Verifiable();

//            TestMock.StripeAccountService.Setup(accountService => accountService.UpdateIndividualAsync(It.IsAny<string>(), It.IsAny<PersonUpdateOptions>()))
//                .ReturnsAsync(new DomainValidationResult<Account>())
//                .Verifiable();

//            var handler = new UserEmailChangedIntegrationEventHandler(TestMock.StripeService.Object, TestMock.StripeAccountService.Object, mockLogger.Object);

//            var integrationEvent = new UserEmailChangedIntegrationEvent
//            {
//                UserId = Guid.NewGuid().ToString(),
//                Email = new EmailDto
//                {
//                    Address = "gabriel@edoxa.gg"
//                }
//            };

//            // Act
//            await handler.HandleAsync(integrationEvent);

//            // Assert
//            TestMock.StripeService.Verify(stripeService => stripeService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
//            TestMock.StripeAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
//            TestMock.StripeAccountService.Verify(accountService => accountService.UpdateIndividualAsync(It.IsAny<string>(), It.IsAny<PersonUpdateOptions>()), Times.Once);
//            mockLogger.Verify(Times.Once());
//        }
//    }
//}
