// Filename: AttachPaymentMethodAsyncTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.Payment.IntegrationTests.Controllers.Stripe.PaymentMethodsController
{
    public sealed class AttachPaymentMethodAsyncTest : IntegrationTest
    {
        public AttachPaymentMethodAsyncTest(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(string paymentMethodId, AttachStripePaymentMethodRequest request)
        {
            return await _httpClient.PostAsJsonAsync($"api/stripe/payment-methods/{paymentMethodId}/attach", request);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockStripeReferenceService = new Mock<IStripeService>();
                            var mockStripeCustomerService = new Mock<IStripeCustomerService>();
                            var mockStripePaymentMethodService = new Mock<IStripePaymentMethodService>();

                            mockStripeReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                            mockStripeCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()))
                                .ThrowsAsync(new StripeException(HttpStatusCode.BadRequest, new StripeError(), string.Empty));

                            container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeService>().SingleInstance();
                            container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                            container.RegisterInstance(mockStripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync("paymentMethodId", new AttachStripePaymentMethodRequest());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
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
            using var response = await this.ExecuteAsync("paymentMethodId", new AttachStripePaymentMethodRequest());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
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
                                    paymentMethodService => paymentMethodService.AttachPaymentMethodAsync(
                                        It.IsAny<string>(),
                                        It.IsAny<string>(),
                                        It.IsAny<bool>()))
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
                                        }));

                            container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeService>().SingleInstance();
                            container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                            container.RegisterInstance(mockStripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync("paymentMethodId", new AttachStripePaymentMethodRequest());

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
