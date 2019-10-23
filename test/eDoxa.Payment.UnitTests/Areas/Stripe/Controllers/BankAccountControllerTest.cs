// Filename: BankAccountControllerTest.cs
// Date Created: 2019-10-11
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Api.Areas.Stripe.Controllers;
using eDoxa.Payment.Api.Areas.Stripe.Requests;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;
using eDoxa.Payment.TestHelpers.Mocks;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.Payment.UnitTests.Areas.Stripe.Controllers
{
    public sealed class BankAccountControllerTest : UnitTest
    {
        public BankAccountControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockExternalService = new Mock<IStripeExternalAccountService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()))
                .ReturnsAsync("accountId")
                .Verifiable();

            mockExternalService.Setup(externalService => externalService.FindBankAccountAsync(It.IsAny<string>()))
                .ReturnsAsync(new BankAccount())
                .Verifiable();

            var bankAccountController = new BankAccountController(mockExternalService.Object, mockAccountService.Object, mockReferenceService.Object, TestMapper);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            bankAccountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await bankAccountController.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
            mockExternalService.Verify(externalService => externalService.FindBankAccountAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockExternalService = new Mock<IStripeExternalAccountService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            var bankAccountController = new BankAccountController(mockExternalService.Object, mockAccountService.Object, mockReferenceService.Object, TestMapper);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            bankAccountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await bankAccountController.GetAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockExternalService = new Mock<IStripeExternalAccountService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()))
                .ThrowsAsync(new StripeException())
                .Verifiable();

            var bankAccountController = new BankAccountController(mockExternalService.Object, mockAccountService.Object, mockReferenceService.Object, TestMapper);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            bankAccountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await bankAccountController.GetAsync();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockExternalService = new Mock<IStripeExternalAccountService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()))
                .ReturnsAsync("accountId")
                .Verifiable();

            mockExternalService.Setup(externalService => externalService.UpdateBankAccountAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new BankAccount())
                .Verifiable();

            var bankAccountController = new BankAccountController(mockExternalService.Object, mockAccountService.Object, mockReferenceService.Object, TestMapper);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            bankAccountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await bankAccountController.PostAsync(new BankAccountPostRequest("AS123TOKEN"));

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
            mockExternalService.Verify(externalService => externalService.UpdateBankAccountAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockExternalService = new Mock<IStripeExternalAccountService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            var bankAccountController = new BankAccountController(mockExternalService.Object, mockAccountService.Object, mockReferenceService.Object, TestMapper);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            bankAccountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await bankAccountController.PostAsync(new BankAccountPostRequest("AS123TOKEN"));

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockExternalService = new Mock<IStripeExternalAccountService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()))
                .ThrowsAsync(new StripeException())
                .Verifiable();

            var bankAccountController = new BankAccountController(mockExternalService.Object, mockAccountService.Object, mockReferenceService.Object, TestMapper);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            bankAccountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await bankAccountController.PostAsync(new BankAccountPostRequest("AS123TOKEN"));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
        }
    }
}
