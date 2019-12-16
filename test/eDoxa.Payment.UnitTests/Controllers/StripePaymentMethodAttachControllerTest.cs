// Filename: StripePaymentMethodAttachControllerTest.cs
// Date Created: 2019-10-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Payment.Requests;
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
    public sealed class StripePaymentMethodAttachControllerTest : UnitTest
    {
        public StripePaymentMethodAttachControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }
        
        [Fact]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var paymentMethodAttachController = new StripePaymentMethodAttachController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodAttachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodAttachController.PostAsync("PaymentMethod", new AttachStripePaymentMethodRequest());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("customerId").Verifiable();

            mockPaymentMethodService
                .Setup(paymentMethodService => paymentMethodService.AttachPaymentMethodAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(new PaymentMethod
                {
                    Id = "PaymentMethodId",
                    Type = "card",
                    Card = new PaymentMethodCard
                    {
                        Brand = "Brand",
                        Country = "CA",
                        Last4 = "1234",
                        ExpMonth = 11,
                        ExpYear = 22
                    }
                })
                .Verifiable();

            var paymentMethodAttachController = new StripePaymentMethodAttachController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodAttachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodAttachController.PostAsync("paymentMethodId", new AttachStripePaymentMethodRequest());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);

            mockPaymentMethodService.Verify(
                paymentMethodService => paymentMethodService.AttachPaymentMethodAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()),
                Times.Once);
        }
    }
}
