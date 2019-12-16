// Filename: StripePaymentMethodsControllerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
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
    public sealed class StripePaymentMethodsControllerTest : UnitTest
    {
        public StripePaymentMethodsControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("customerId").Verifiable();

            mockPaymentMethodService.Setup(paymentMethodService => paymentMethodService.FetchPaymentMethodsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(
                    new StripeList<PaymentMethod>
                    {
                        Data = new List<PaymentMethod>()
                    })
                .Verifiable();

            var paymentMethodController = new StripePaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodController.GetAsync("card");

            // Assert
            result.Should().BeOfType<NoContentResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);

            mockPaymentMethodService.Verify(
                paymentMethodService => paymentMethodService.FetchPaymentMethodsAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var paymentMethodController = new StripePaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodController.GetAsync("type");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("customerId").Verifiable();

            mockPaymentMethodService.Setup(paymentMethodService => paymentMethodService.FetchPaymentMethodsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(
                    new StripeList<PaymentMethod>
                    {
                        Data = new List<PaymentMethod>
                        {
                            new PaymentMethod
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
                            }
                        }
                    })
                .Verifiable();

            var paymentMethodController = new StripePaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodController.GetAsync("card");

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);

            mockPaymentMethodService.Verify(
                paymentMethodService => paymentMethodService.FetchPaymentMethodsAsync(It.IsAny<string>(), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var paymentMethodController = new StripePaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodController.PutAsync(
                "type",
                new UpdateStripePaymentMethodRequest
                {
                    ExpYear = 22,
                    ExpMonth = 11
                });

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

            mockPaymentMethodService
                .Setup(paymentMethodService => paymentMethodService.UpdatePaymentMethodAsync(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
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

            var paymentMethodController = new StripePaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodController.PutAsync(
                "type",
                new UpdateStripePaymentMethodRequest
                {
                    ExpYear = 22,
                    ExpMonth = 11
                });

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>()), Times.Once);

            mockPaymentMethodService.Verify(
                paymentMethodService => paymentMethodService.UpdatePaymentMethodAsync(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()),
                Times.Once);
        }
    }
}
