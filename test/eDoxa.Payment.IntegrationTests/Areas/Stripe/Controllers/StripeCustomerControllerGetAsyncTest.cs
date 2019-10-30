// Filename: StripeCustomerControllerGetAsyncTest.cs
// Date Created: 2019-10-24
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Payment.Api.Areas.Stripe.Requests;
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
    public sealed class StripeCustomerControllerGetAsyncTest : IntegrationTest
    {
        public StripeCustomerControllerGetAsyncTest(TestApiFixture testApi, TestMapperFixture testMapper) : base(testApi, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync($"api/stripe/customer");
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
                        var mockStripeCustomerService = new Mock<IStripeCustomerService>();

                        mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                        mockStripeCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("customerId");

                        mockStripeCustomerService.Setup(customerService => customerService.FindCustomerAsync(It.IsAny<string>())).ReturnsAsync(new Customer());

                        container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                        container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                    }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync();

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
                        var mockStripeCustomerService = new Mock<IStripeCustomerService>();

                        mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false);

                        container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                        container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                    }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync();

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
                        var mockStripeCustomerService = new Mock<IStripeCustomerService>();

                        mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                        mockStripeCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ThrowsAsync(new StripeException());

                        container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                        container.RegisterInstance(mockStripeCustomerService.Object).As<IStripeCustomerService>().SingleInstance();
                    }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
