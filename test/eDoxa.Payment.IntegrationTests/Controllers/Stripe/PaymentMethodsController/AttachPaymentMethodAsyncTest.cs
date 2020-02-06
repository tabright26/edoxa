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
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.Stripe.Services.Abstractions;

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

        //[Fact]
        //public async Task ShouldBeHttpStatusCodeBadRequest()
        //{
        //    // Arrange
        //    var userId = new UserId();

        //    var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(CustomClaimTypes.StripeCustomer, "customerId"));

        //    _httpClient = factory.CreateClient();

        //    // Act
        //    using var response = await this.ExecuteAsync("paymentMethodId", new AttachStripePaymentMethodRequest());

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        //}

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();

            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(CustomClaimTypes.StripeCustomer, "customerId"))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            TestMock.StripePaymentMethodService
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

                            container.RegisterInstance(TestMock.StripePaymentMethodService.Object).As<IStripePaymentMethodService>().SingleInstance();
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
