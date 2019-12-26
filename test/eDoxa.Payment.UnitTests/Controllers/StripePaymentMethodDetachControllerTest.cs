// Filename: StripePaymentMethodDetachControllerTest.cs
// Date Created: 2019-10-15
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
    public sealed class StripePaymentMethodDetachControllerTest : UnitTest
    {
        public StripePaymentMethodDetachControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var paymentMethodDetachController = new StripePaymentMethodDetachController(
                mockPaymentMethodService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodDetachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodDetachController.PostAsync("PaymentMethod");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockPaymentMethodService.Setup(paymentMethodService => paymentMethodService.DetachPaymentMethodAsync(It.IsAny<string>()))
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

            var paymentMethodDetachController = new StripePaymentMethodDetachController(
                mockPaymentMethodService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodDetachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodDetachController.PostAsync("PaymentMethod");

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockPaymentMethodService.Verify(paymentMethodService => paymentMethodService.DetachPaymentMethodAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
