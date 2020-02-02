// Filename: StripePaymentMethodsControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Payment.Api.Controllers.Stripe;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Payment.TestHelper.Mocks;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.Payment.UnitTests.Controllers.Stripe
{
    public sealed class PaymentMethodsControllerTest : UnitTest
    {
        public PaymentMethodsControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task FetchPaymentMethodsAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("customerId").Verifiable();

            mockPaymentMethodService.Setup(paymentMethodService => paymentMethodService.FetchPaymentMethodsAsync(It.IsAny<string>()))
                .ReturnsAsync(
                    new StripeList<PaymentMethod>
                    {
                        Data = new List<PaymentMethod>()
                    })
                .Verifiable();

            var paymentMethodController = new PaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodController.FetchPaymentMethodsAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);

            mockPaymentMethodService.Verify(paymentMethodService => paymentMethodService.FetchPaymentMethodsAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task FetchPaymentMethodsAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var paymentMethodController = new PaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodController.FetchPaymentMethodsAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FetchPaymentMethodsAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("customerId").Verifiable();

            mockPaymentMethodService.Setup(paymentMethodService => paymentMethodService.FetchPaymentMethodsAsync(It.IsAny<string>()))
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

            var paymentMethodController = new PaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodController.FetchPaymentMethodsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);

            mockPaymentMethodService.Verify(paymentMethodService => paymentMethodService.FetchPaymentMethodsAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UpdatePaymentMethodAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var paymentMethodController = new PaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodController.UpdatePaymentMethodAsync(
                "type",
                new UpdateStripePaymentMethodRequest
                {
                    ExpYear = 22,
                    ExpMonth = 11
                });

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task UpdatePaymentMethodAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockPaymentMethodService
                .Setup(paymentMethodService => paymentMethodService.UpdatePaymentMethodAsync(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(
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
                    })
                .Verifiable();

            var paymentMethodController = new PaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodController.UpdatePaymentMethodAsync(
                "type",
                new UpdateStripePaymentMethodRequest
                {
                    ExpYear = 22,
                    ExpMonth = 11
                });

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);

            mockPaymentMethodService.Verify(
                paymentMethodService => paymentMethodService.UpdatePaymentMethodAsync(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()),
                Times.Once);
        }

        [Fact]
        public async Task AttachPaymentMethodAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var paymentMethodAttachController = new PaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodAttachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodAttachController.AttachPaymentMethodAsync("PaymentMethod", new AttachStripePaymentMethodRequest());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task AttachPaymentMethodAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("customerId").Verifiable();

            mockPaymentMethodService
                .Setup(paymentMethodService => paymentMethodService.AttachPaymentMethodAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(
                    DomainValidationResult<PaymentMethod>.Succeeded(
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
                        }))
                .Verifiable();

            var paymentMethodAttachController = new PaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodAttachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodAttachController.AttachPaymentMethodAsync("paymentMethodId", new AttachStripePaymentMethodRequest());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);

            mockPaymentMethodService.Verify(
                paymentMethodService => paymentMethodService.AttachPaymentMethodAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()),
                Times.Once);
        }

        [Fact]
        public async Task DetachPaymentMethodAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var paymentMethodDetachController = new PaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodDetachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodDetachController.DetachPaymentMethodAsync("PaymentMethod");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DetachPaymentMethodAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockPaymentMethodService = new Mock<IStripePaymentMethodService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockReferenceService = new Mock<IStripeService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockPaymentMethodService.Setup(paymentMethodService => paymentMethodService.DetachPaymentMethodAsync(It.IsAny<string>()))
                .ReturnsAsync(
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
                    })
                .Verifiable();

            var paymentMethodDetachController = new PaymentMethodsController(
                mockPaymentMethodService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            paymentMethodDetachController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await paymentMethodDetachController.DetachPaymentMethodAsync("PaymentMethod");

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockPaymentMethodService.Verify(paymentMethodService => paymentMethodService.DetachPaymentMethodAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task SetDefaultPaymentMethodAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockReferenceService = new Mock<IStripeService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockStripeService = new Mock<IStripePaymentMethodService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            var customerPaymentDefaultController = new PaymentMethodsController(
                mockStripeService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            customerPaymentDefaultController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await customerPaymentDefaultController.SetDefaultPaymentMethodAsync("testValue");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task SetDefaultPaymentMethodAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockReferenceService = new Mock<IStripeService>();
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockStripeService = new Mock<IStripePaymentMethodService>();

            mockReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

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

            var customerPaymentDefaultController = new PaymentMethodsController(
                mockStripeService.Object,
                mockCustomerService.Object,
                mockReferenceService.Object,
                TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            customerPaymentDefaultController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await customerPaymentDefaultController.SetDefaultPaymentMethodAsync("testValue");

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockReferenceService.Verify(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.SetDefaultPaymentMethodAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
