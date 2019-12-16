// Filename: StripeCustomerPaymentMethodDefaultControllerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Api.Controllers;
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

namespace eDoxa.Payment.UnitTests.Controllers
{
    public sealed class StripeCustomerPaymentMethodDefaultControllerTest : UnitTest
    {
        public StripeCustomerPaymentMethodDefaultControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task PutAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockReferenceService = new Mock<IStripeReferenceService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var customerPaymentDefaultController = new StripeCustomerPaymentMethodDefaultController(
                mockReferenceService.Object,
                mockCustomerService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            customerPaymentDefaultController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await customerPaymentDefaultController.PutAsync("testValue");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockReferenceService = new Mock<IStripeReferenceService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("customerId").Verifiable();

            mockCustomerService.Setup(customerService => customerService.SetDefaultPaymentMethodAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(
                    new Customer
                    {
                        InvoiceSettings = new CustomerInvoiceSettings
                        {
                            DefaultPaymentMethodId = "DefaultPaymentMethodId"
                        }
                    })
                .Verifiable();

            var customerPaymentDefaultController = new StripeCustomerPaymentMethodDefaultController(
                mockReferenceService.Object,
                mockCustomerService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            customerPaymentDefaultController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await customerPaymentDefaultController.PutAsync("testValue");

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.SetDefaultPaymentMethodAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
