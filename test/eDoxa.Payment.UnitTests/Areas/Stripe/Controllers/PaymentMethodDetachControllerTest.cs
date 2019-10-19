﻿// Filename: PaymentMethodDetachControllerTest.cs
// Date Created: 2019-10-11
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Api.Areas.Stripe.Controllers;
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
    public sealed class PaymentMethodDetachControllerTest : UnitTest
    {
        public PaymentMethodDetachControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockPaymentMethodService.Setup(paymentMethodService => paymentMethodService.DetachPaymentMethodAsync(It.IsAny<string>()))
                .ReturnsAsync(new PaymentMethod())
                .Verifiable();

            var paymentMethodDetachController = new PaymentMethodDetachController(mockPaymentMethodService.Object, mockReferenceService.Object);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodDetachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodDetachController.PostAsync("PaymentMethod");

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockPaymentMethodService.Verify(paymentMethodService => paymentMethodService.DetachPaymentMethodAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(false)
                .Verifiable();

            var paymentMethodDetachController = new PaymentMethodDetachController(mockPaymentMethodService.Object, mockReferenceService.Object);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodDetachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodDetachController.PostAsync("PaymentMethod");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(true)
                .Verifiable();

            mockPaymentMethodService.Setup(paymentMethodService => paymentMethodService.DetachPaymentMethodAsync(It.IsAny<string>()))
                .ThrowsAsync(new StripeException())
                .Verifiable();

            var paymentMethodDetachController = new PaymentMethodDetachController(mockPaymentMethodService.Object, mockReferenceService.Object);
            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodDetachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodDetachController.PostAsync("PaymentMethod");

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockPaymentMethodService.Verify(paymentMethodService => paymentMethodService.DetachPaymentMethodAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
