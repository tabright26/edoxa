﻿// Filename: PaymentMethodsControllerGetAsyncTest.cs
// Date Created: 2019-10-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Stripe;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Payment.IntegrationTests.Areas.Stripe.Controllers
{
    public sealed class StripePaymentMethodsControllerGetAsyncTest : IntegrationTest // GABRIEL: INTEGRATION TEST
    {
        public StripePaymentMethodsControllerGetAsyncTest(TestApiFixture testApi, TestMapperFixture testMapper) : base(testApi, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/stripe/payment-methods");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockStripeReferenceService = new Mock<IStripeReferenceService>();
                            var mockStripeCustomerService = new Mock<IStripeCustomerService>();
                            var mockStripePaymentMethodService = new Mock<IStripePaymentMethodService>();

                            mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                            mockStripeCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()))
                                .ThrowsAsync(new StripeException());

                            container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                            container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                            container.RegisterInstance(mockStripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockStripeReferenceService = new Mock<IStripeReferenceService>();
                            var mockStripeCustomerService = new Mock<IStripeCustomerService>();
                            var mockStripePaymentMethodService = new Mock<IStripePaymentMethodService>();

                            mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false);

                            container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                            container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                            container.RegisterInstance(mockStripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mockStripeReferenceService = new Mock<IStripeReferenceService>();
                            var mockStripeCustomerService = new Mock<IStripeCustomerService>();
                            var mockStripePaymentMethodService = new Mock<IStripePaymentMethodService>();

                            mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                            mockStripeCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()))
                                .ReturnsAsync("customerId");

                            mockStripePaymentMethodService
                                .Setup(paymentMethodService => paymentMethodService.FetchPaymentMethodsAsync(It.IsAny<string>(), It.IsAny<string>()))
                                .ReturnsAsync(
                                    new StripeList<PaymentMethod>
                                    {
                                        Data = new List<PaymentMethod>
                                        {
                                            new PaymentMethod
                                            {
                                                Card = new PaymentMethodCard()
                                            }
                                        }
                                    });

                            container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                            container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                            container.RegisterInstance(mockStripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}