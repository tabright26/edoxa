// Filename: PaymentMethodAttachControllerTest.cs
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
    public sealed class PaymentMethodAttachControllerTest : UnitTest
    {
        public PaymentMethodAttachControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()))
                .ReturnsAsync("customerId")
                .Verifiable();

            mockPaymentMethodService.Setup(paymentMethodService => paymentMethodService.AttachPaymentMethodAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(new PaymentMethod())
                .Verifiable();

            var paymentMethodAttachController = new PaymentMethodAttachController(mockPaymentMethodService.Object, mockCustomerService.Object, mockReferenceService.Object);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodAttachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodAttachController.PostAsync("paymentMethodId", new PaymentMethodAttachPostRequest());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);
            mockPaymentMethodService.Verify(paymentMethodService => paymentMethodService.AttachPaymentMethodAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            var paymentMethodAttachController = new PaymentMethodAttachController(mockPaymentMethodService.Object, mockCustomerService.Object, mockReferenceService.Object);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodAttachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodAttachController.PostAsync("PaymentMethod", new PaymentMethodAttachPostRequest());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()))
                .ThrowsAsync(new StripeException())
                .Verifiable();

            var paymentMethodAttachController = new PaymentMethodAttachController(mockPaymentMethodService.Object, mockCustomerService.Object, mockReferenceService.Object);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodAttachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodAttachController.PostAsync("PaymentMethod", new PaymentMethodAttachPostRequest());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);
        }
    }
}
