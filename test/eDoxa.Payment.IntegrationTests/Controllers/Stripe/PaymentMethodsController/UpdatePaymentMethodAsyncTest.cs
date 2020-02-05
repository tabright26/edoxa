﻿// Filename: UpdatePaymentMethodAsyncTest.cs
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
using eDoxa.Payment.Api.Application.Stripe.Services.Abstractions;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
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
    public sealed class UpdatePaymentMethodAsyncTest : IntegrationTest
    {
        public UpdatePaymentMethodAsyncTest(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
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

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            TestMock.StripePaymentMethodService
                                .Setup(
                                    paymentMethodService => paymentMethodService.UpdatePaymentMethodAsync(
                                        It.IsAny<string>(),
                                        It.IsAny<long>(),
                                        It.IsAny<long>()))
                                .ThrowsAsync(new StripeException(HttpStatusCode.BadRequest, new StripeError(), string.Empty));

                            container.RegisterInstance(TestMock.StripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
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

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()));

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

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            TestMock.StripePaymentMethodService
                                .Setup(
                                    paymentMethodService => paymentMethodService.UpdatePaymentMethodAsync(
                                        It.IsAny<string>(),
                                        It.IsAny<long>(),
                                        It.IsAny<long>()))
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
                                    });

                            container.RegisterInstance(TestMock.StripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
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
