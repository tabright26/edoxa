// Filename: SetDefaultPaymentMethodAsyncTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
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
    public sealed class SetDefaultPaymentMethodAsyncTest : IntegrationTest
    {
        public SetDefaultPaymentMethodAsyncTest(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(string paymentMethodId)
        {
            return await _httpClient.PutAsJsonAsync(
                $"api/stripe/payment-methods/{paymentMethodId}/default",
                new
                {
                });
        }

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
                            TestMock.StripeCustomerService.Setup(
                                    customerService => customerService.SetDefaultPaymentMethodAsync(It.IsAny<string>(), It.IsAny<string>()))
                                .ReturnsAsync(
                                    new Customer
                                    {
                                        InvoiceSettings = new CustomerInvoiceSettings
                                        {
                                            DefaultPaymentMethodId = "DefaultPaymentMethodId"
                                        }
                                    });

                            container.RegisterInstance(TestMock.StripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync("paymentMethodId");

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
