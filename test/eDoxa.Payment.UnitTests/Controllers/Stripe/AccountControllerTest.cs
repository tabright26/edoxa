// Filename: AccountControllerTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Api.Controllers.Stripe;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.Payment.UnitTests.Controllers.Stripe
{
    public sealed class AccountControllerTest : UnitTest
    {
        public AccountControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task FetchAccountAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            TestMock.StripeService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var accountController = new AccountController(TestMock.StripeAccountService.Object, TestMock.StripeService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await accountController.FetchAccountAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.StripeService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FetchAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            TestMock.StripeService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            TestMock.StripeAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("accountId").Verifiable();

            TestMock.StripeAccountService.Setup(accountService => accountService.GetAccountAsync(It.IsAny<string>())).ReturnsAsync(new Account()).Verifiable();

            var accountController = new AccountController(TestMock.StripeAccountService.Object, TestMock.StripeService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await accountController.FetchAccountAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            TestMock.StripeService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.StripeAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.StripeAccountService.Verify(accountService => accountService.GetAccountAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
