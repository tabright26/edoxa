// Filename: StripePaymentMethodsControllerPutAsyncTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.Payment.IntegrationTests.Controllers
{
    public sealed class StripePaymentMethodsControllerPutAsyncTest : IntegrationTest
    {
        public StripePaymentMethodsControllerPutAsyncTest(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(string paymentMethodId, UpdateStripePaymentMethodRequest request)
        {
            return await _httpClient.PutAsJsonAsync($"api/stripe/payment-methods/{paymentMethodId}", request);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockStripeReferenceService = new Mock<IStripeService>();
                            var mockStripeCustomerService = new Mock<IStripeCustomerService>();
                            var mockStripePaymentMethodService = new Mock<IStripePaymentMethodService>();

                            mockStripeReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                            mockStripePaymentMethodService
                                .Setup(
                                    paymentMethodService => paymentMethodService.UpdatePaymentMethodAsync(
                                        It.IsAny<string>(),
                                        It.IsAny<long>(),
                                        It.IsAny<long>()))
                                .ThrowsAsync(new StripeException(HttpStatusCode.BadRequest, new StripeError(), string.Empty));

                            container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeService>().SingleInstance();
                            container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                            container.RegisterInstance(mockStripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(
                "paymentMethodId",
                new UpdateStripePaymentMethodRequest
                {
                    ExpMonth = 11,
                    ExpYear = 22
                });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockStripeReferenceService = new Mock<IStripeService>();
                            var mockStripeCustomerService = new Mock<IStripeCustomerService>();
                            var mockStripePaymentMethodService = new Mock<IStripePaymentMethodService>();

                            mockStripeReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false);

                            container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeService>().SingleInstance();
                            container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                            container.RegisterInstance(mockStripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(
                "paymentMethodId",
                new UpdateStripePaymentMethodRequest
                {
                    ExpMonth = 11,
                    ExpYear = 22
                });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockStripeReferenceService = new Mock<IStripeService>();
                            var mockStripeCustomerService = new Mock<IStripeCustomerService>();
                            var mockStripePaymentMethodService = new Mock<IStripePaymentMethodService>();

                            mockStripeReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                            mockStripeCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()))
                                .ReturnsAsync("customerId");

                            mockStripePaymentMethodService
                                .Setup(
                                    paymentMethodService => paymentMethodService.UpdatePaymentMethodAsync(
                                        It.IsAny<string>(),
                                        It.IsAny<long>(),
                                        It.IsAny<long>()))
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
                                });

                            container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeService>().SingleInstance();
                            container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                            container.RegisterInstance(mockStripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(
                "paymentMethodId",
                new UpdateStripePaymentMethodRequest
                {
                    ExpMonth = 11,
                    ExpYear = 22
                });

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
