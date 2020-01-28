// Filename: StripeBankAccountControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Payment.Api.Controllers.Stripe;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Payment.TestHelper.Mocks;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.Payment.UnitTests.Controllers.Stripe
{
    public sealed class BankAccountControllerTest : UnitTest
    {
        public BankAccountControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task FetchBankAccountAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockExternalService = new Mock<IStripeExternalAccountService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var bankAccountController = new BankAccountController(
                mockExternalService.Object,
                mockAccountService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            bankAccountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await bankAccountController.FetchBankAccountAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FetchBankAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockExternalService = new Mock<IStripeExternalAccountService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("accountId").Verifiable();

            mockExternalService.Setup(externalService => externalService.FindBankAccountAsync(It.IsAny<string>()))
                .ReturnsAsync(
                    new BankAccount
                    {
                        BankName = "BankName",
                        Country = "CA",
                        Currency = "CAD",
                        Last4 = "1234",
                        Status = "pending",
                        DefaultForCurrency = true
                    })
                .Verifiable();

            var bankAccountController = new BankAccountController(
                mockExternalService.Object,
                mockAccountService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            bankAccountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await bankAccountController.FetchBankAccountAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
            mockExternalService.Verify(externalService => externalService.FindBankAccountAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task FetchBankAccountAsync_WhenBankAccountDoesNotExist_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockExternalService = new Mock<IStripeExternalAccountService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("accountId").Verifiable();

            mockExternalService.Setup(externalService => externalService.FindBankAccountAsync(It.IsAny<string>())).Verifiable();

            var bankAccountController = new BankAccountController(
                mockExternalService.Object,
                mockAccountService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            bankAccountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await bankAccountController.FetchBankAccountAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
            mockExternalService.Verify(externalService => externalService.FindBankAccountAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBankAccountAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockExternalService = new Mock<IStripeExternalAccountService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var bankAccountController = new BankAccountController(
                mockExternalService.Object,
                mockAccountService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            bankAccountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await bankAccountController.UpdateBankAccountAsync(
                new CreateStripeBankAccountRequest
                {
                    Token = "AS123TOKEN"
                });

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBankAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockExternalService = new Mock<IStripeExternalAccountService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("accountId").Verifiable();

            mockExternalService.Setup(externalService => externalService.UpdateBankAccountAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(
                    new BankAccount
                    {
                        BankName = "BankName",
                        Country = "CA",
                        Currency = "CAD",
                        Last4 = "1234",
                        Status = "pending",
                        DefaultForCurrency = true
                    })
                .Verifiable();

            var bankAccountController = new BankAccountController(
                mockExternalService.Object,
                mockAccountService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            bankAccountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await bankAccountController.UpdateBankAccountAsync(
                new CreateStripeBankAccountRequest
                {
                    Token = "AS123TOKEN"
                });

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
            mockExternalService.Verify(externalService => externalService.UpdateBankAccountAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
