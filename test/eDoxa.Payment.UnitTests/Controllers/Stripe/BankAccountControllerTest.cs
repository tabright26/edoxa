//// Filename: BankAccountControllerTest.cs
//// Date Created: 2020-01-28
//// 
//// ================================================
//// Copyright © 2020, eDoxa. All rights reserved.

//using System.Threading.Tasks;

//using eDoxa.Grpc.Protos.Payment.Requests;
//using eDoxa.Payment.Api.Controllers.Stripe;
//using eDoxa.Payment.TestHelper;
//using eDoxa.Payment.TestHelper.Fixtures;
//using eDoxa.Seedwork.Domain.Misc;
//using eDoxa.Seedwork.TestHelper.Mocks;

//using FluentAssertions;

//using Microsoft.AspNetCore.Mvc;

//using Moq;

//using Stripe;

//using Xunit;

//namespace eDoxa.Payment.UnitTests.Controllers.Stripe
//{
//    public sealed class BankAccountControllerTest : UnitTest
//    {
//        public BankAccountControllerTest(TestMapperFixture testMapper) : base(testMapper)
//        {
//        }

//        [Fact]
//        public async Task FetchBankAccountAsync_ShouldBeOfTypeNotFoundObjectResult()
//        {
//            // Arrange
//            TestMock.StripeService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

//            var bankAccountController = new BankAccountController(
//                TestMock.StripeExternalAccountService.Object,
//                TestMock.StripeAccountService.Object,
//                TestMock.StripeService.Object,
//                TestMapper)
//            {
//                ControllerContext =
//                {
//                    HttpContext = MockHttpContextAccessor.GetInstance()
//                }
//            };

//            // Act
//            var result = await bankAccountController.FetchBankAccountAsync();

//            // Assert
//            result.Should().BeOfType<NotFoundObjectResult>();
//            TestMock.StripeService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
//        }

//        [Fact]
//        public async Task FetchBankAccountAsync_ShouldBeOfTypeOkObjectResult()
//        {
//            // Arrange
//            TestMock.StripeService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

//            TestMock.StripeAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("accountId").Verifiable();

//            TestMock.StripeExternalAccountService.Setup(externalService => externalService.FindBankAccountAsync(It.IsAny<string>()))
//                .ReturnsAsync(
//                    new BankAccount
//                    {
//                        BankName = "BankName",
//                        Country = "CA",
//                        Currency = "CAD",
//                        Last4 = "1234",
//                        Status = "pending",
//                        DefaultForCurrency = true
//                    })
//                .Verifiable();

//            var bankAccountController = new BankAccountController(
//                TestMock.StripeExternalAccountService.Object,
//                TestMock.StripeAccountService.Object,
//                TestMock.StripeService.Object,
//                TestMapper)
//            {
//                ControllerContext =
//                {
//                    HttpContext = MockHttpContextAccessor.GetInstance()
//                }
//            };

//            // Act
//            var result = await bankAccountController.FetchBankAccountAsync();

//            // Assert
//            result.Should().BeOfType<OkObjectResult>();
//            TestMock.StripeService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
//            TestMock.StripeAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
//            TestMock.StripeExternalAccountService.Verify(externalService => externalService.FindBankAccountAsync(It.IsAny<string>()), Times.Once);
//        }

//        [Fact]
//        public async Task FetchBankAccountAsync_WhenBankAccountDoesNotExist_ShouldBeOfTypeNotFoundObjectResult()
//        {
//            // Arrange
//            TestMock.StripeService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

//            TestMock.StripeAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("accountId").Verifiable();

//            TestMock.StripeExternalAccountService.Setup(externalService => externalService.FindBankAccountAsync(It.IsAny<string>())).Verifiable();

//            var bankAccountController = new BankAccountController(
//                TestMock.StripeExternalAccountService.Object,
//                TestMock.StripeAccountService.Object,
//                TestMock.StripeService.Object,
//                TestMapper)
//            {
//                ControllerContext =
//                {
//                    HttpContext = MockHttpContextAccessor.GetInstance()
//                }
//            };

//            // Act
//            var result = await bankAccountController.FetchBankAccountAsync();

//            // Assert
//            result.Should().BeOfType<NotFoundObjectResult>();
//            TestMock.StripeService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
//            TestMock.StripeAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
//            TestMock.StripeExternalAccountService.Verify(externalService => externalService.FindBankAccountAsync(It.IsAny<string>()), Times.Once);
//        }

//        [Fact]
//        public async Task UpdateBankAccountAsync_ShouldBeOfTypeNotFoundObjectResult()
//        {
//            // Arrange
//            TestMock.StripeService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

//            var bankAccountController = new BankAccountController(
//                TestMock.StripeExternalAccountService.Object,
//                TestMock.StripeAccountService.Object,
//                TestMock.StripeService.Object,
//                TestMapper)
//            {
//                ControllerContext =
//                {
//                    HttpContext = MockHttpContextAccessor.GetInstance()
//                }
//            };

//            // Act
//            var result = await bankAccountController.UpdateBankAccountAsync(
//                new CreateStripeBankAccountRequest
//                {
//                    Token = "AS123TOKEN"
//                });

//            // Assert
//            result.Should().BeOfType<NotFoundObjectResult>();
//            TestMock.StripeService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
//        }

//        [Fact]
//        public async Task UpdateBankAccountAsync_ShouldBeOfTypeOkObjectResult()
//        {
//            // Arrange
//            TestMock.StripeService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

//            TestMock.StripeAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("accountId").Verifiable();

//            TestMock.StripeExternalAccountService.Setup(externalService => externalService.ChangeBankAccountAsync(It.IsAny<string>(), It.IsAny<string>()))
//                .ReturnsAsync(
//                    new BankAccount
//                    {
//                        BankName = "BankName",
//                        Country = "CA",
//                        Currency = "CAD",
//                        Last4 = "1234",
//                        Status = "pending",
//                        DefaultForCurrency = true
//                    })
//                .Verifiable();

//            var bankAccountController = new BankAccountController(
//                TestMock.StripeExternalAccountService.Object,
//                TestMock.StripeAccountService.Object,
//                TestMock.StripeService.Object,
//                TestMapper)
//            {
//                ControllerContext =
//                {
//                    HttpContext = MockHttpContextAccessor.GetInstance()
//                }
//            };

//            // Act
//            var result = await bankAccountController.UpdateBankAccountAsync(
//                new CreateStripeBankAccountRequest
//                {
//                    Token = "AS123TOKEN"
//                });

//            // Assert
//            result.Should().BeOfType<OkObjectResult>();
//            TestMock.StripeService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
//            TestMock.StripeAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
//            TestMock.StripeExternalAccountService.Verify(
//                externalService => externalService.ChangeBankAccountAsync(It.IsAny<string>(), It.IsAny<string>()),
//                Times.Once);
//        }
//    }
//}
