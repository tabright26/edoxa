﻿// Filename: PaymentMethodDetachControllerPostAsyncTest.cs
// Date Created: 2019-10-11
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Testing.Http;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Stripe;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Payment.IntegrationTests.Areas.Stripe.Controllers
{
    public sealed class StripePaymentMethodDetachControllerPostAsyncTest : IntegrationTest // GABRIEL: INTEGRATION TEST
    {
        public StripePaymentMethodDetachControllerPostAsyncTest(TestApiFixture testApi, TestMapperFixture testMapper) : base(testApi, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(string paymentMethodId)
        {
            return await _httpClient.PostAsync($"api/stripe/payment-methods/{paymentMethodId}/detach", new JsonContent(""));
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();
            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(builder => builder.ConfigureTestContainer<ContainerBuilder>(
                container =>
                {
                    var mockStripeReferenceService = new Mock<IStripeReferenceService>();
                    var mockStripePaymentMethodService = new Mock<IStripePaymentMethodService>();

                    mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                    mockStripePaymentMethodService.Setup(paymentMethodService => paymentMethodService.DetachPaymentMethodAsync(It.IsAny<string>()))
                        .ReturnsAsync(new PaymentMethod());

                    container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                    container.RegisterInstance(mockStripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
                }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync("paymentMethodId");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

                [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var userId = new UserId();
            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(builder => builder.ConfigureTestContainer<ContainerBuilder>(
                container =>
                {
                    var mockStripeReferenceService = new Mock<IStripeReferenceService>();
                    var mockStripePaymentMethodService = new Mock<IStripePaymentMethodService>();

                    mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false);


                    container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                    container.RegisterInstance(mockStripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
                }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync("paymentMethodId");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var userId = new UserId();
            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(builder => builder.ConfigureTestContainer<ContainerBuilder>(
                container =>
                {
                    var mockStripeReferenceService = new Mock<IStripeReferenceService>();
                    var mockStripePaymentMethodService = new Mock<IStripePaymentMethodService>();

                    mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                    mockStripePaymentMethodService.Setup(paymentMethodService => paymentMethodService.DetachPaymentMethodAsync(It.IsAny<string>()))
                        .ThrowsAsync(new StripeException());

                    container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                    container.RegisterInstance(mockStripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
                }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync("paymentMethodId");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}