// Filename: AccountControllerTest.cs
// Date Created: 2019-10-11
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Payment.Api.Areas.Stripe.Controllers;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;
using eDoxa.Payment.TestHelpers.Mocks;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.Payment.UnitTests.Areas.Stripe.Controllers
{
    public sealed class AccountControllerTest : UnitTest
    {
        public AccountControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()))
                .ReturnsAsync("accountId")
                .Verifiable();

            mockAccountService.Setup(accountService => accountService.GetAccountAsync(It.IsAny<string>()))
                .ReturnsAsync(new Account())
                .Verifiable();

            var accountController = new AccountController(mockAccountService.Object, mockReferenceService.Object);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            accountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await accountController.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.GetAccountAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            var accountController = new AccountController(mockAccountService.Object, mockReferenceService.Object);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            accountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await accountController.GetAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()))
                .ThrowsAsync(new StripeException())
                .Verifiable();

            var accountController = new AccountController(mockAccountService.Object, mockReferenceService.Object);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            accountController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await accountController.GetAsync();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
        }
    }
}

