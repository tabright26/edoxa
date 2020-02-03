// Filename: CustomerControllerTest.cs
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
    public sealed class CustomerControllerTest : UnitTest
    {
        public CustomerControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task FetchCustomerAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            TestMock.StripeService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var customerController = new CustomerController(TestMock.StripeCustomerService.Object, TestMock.StripeService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await customerController.FetchCustomerAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.StripeService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FetchCustomerAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            TestMock.StripeService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            TestMock.StripeCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()))
                .ReturnsAsync("customerID")
                .Verifiable();

            TestMock.StripeCustomerService.Setup(customerService => customerService.FindCustomerAsync(It.IsAny<string>()))
                .ReturnsAsync(
                    new Customer
                    {
                        InvoiceSettings = new CustomerInvoiceSettings
                        {
                            DefaultPaymentMethodId = "DefaultPaymentMethodId"
                        }
                    })
                .Verifiable();

            var customerController = new CustomerController(TestMock.StripeCustomerService.Object, TestMock.StripeService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await customerController.FetchCustomerAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            TestMock.StripeService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.StripeCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.StripeCustomerService.Verify(customerService => customerService.FindCustomerAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
