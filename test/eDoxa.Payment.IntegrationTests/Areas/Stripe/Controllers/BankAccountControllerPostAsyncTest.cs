// Filename: BankAccountControllerPostAsyncTest.cs
// Date Created: 2019-10-11
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
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

namespace eDoxa.Payment.IntegrationTests.Areas.Stripe.Controllers
{
    public sealed class BankAccountControllerPostAsyncTest : IntegrationTest
    {
        public BankAccountControllerPostAsyncTest(TestApiFixture testApi, TestMapperFixture testMapper) : base(testApi, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(BankAccountPostRequest request)
        {
            return await _httpClient.PostAsync($"api/stripe/bank-account", new JsonContent(request));
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
                    var mockStripeAccountService = new Mock<IStripeAccountService>();
                    var mockStripeExternalAccountService = new Mock<IStripeExternalAccountService>();

                    mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                    mockStripeAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("acct_123eqwqw12");

                    mockStripeExternalAccountService.Setup(externalAccountService => externalAccountService.UpdateBankAccountAsync(It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync(new BankAccount());

                    container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                    container.RegisterInstance(mockStripeAccountService.Object).As<IStripeAccountService>().SingleInstance();
                    container.RegisterInstance(mockStripeExternalAccountService.Object).As<IStripeExternalAccountService>().SingleInstance();
                }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(new BankAccountPostRequest("123TokenGG"));

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
                    var mockStripeAccountService = new Mock<IStripeAccountService>();
                    var mockStripeExternalAccountService = new Mock<IStripeExternalAccountService>();

                    mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false);

                    container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                    container.RegisterInstance(mockStripeAccountService.Object).As<IStripeAccountService>().SingleInstance();
                    container.RegisterInstance(mockStripeExternalAccountService.Object).As<IStripeExternalAccountService>().SingleInstance();
                }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(new BankAccountPostRequest("123TokenGG"));

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
                    var mockStripeAccountService = new Mock<IStripeAccountService>();
                    var mockStripeExternalAccountService = new Mock<IStripeExternalAccountService>();

                    mockStripeReferenceService.Setup(referenceService => referenceService.ReferenceExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                    mockStripeAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ThrowsAsync(new StripeException());

                    container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeReferenceService>().SingleInstance();
                    container.RegisterInstance(mockStripeAccountService.Object).As<IStripeAccountService>().SingleInstance();
                    container.RegisterInstance(mockStripeExternalAccountService.Object).As<IStripeExternalAccountService>().SingleInstance();
                }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(new BankAccountPostRequest("123TokenGG"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
