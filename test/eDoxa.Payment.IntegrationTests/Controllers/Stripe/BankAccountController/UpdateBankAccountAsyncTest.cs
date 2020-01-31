// Filename: UpdateBankAccountAsyncTest.cs
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
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.Payment.IntegrationTests.Controllers.Stripe.BankAccountController
{
    public sealed class UpdateBankAccountAsyncTest : IntegrationTest
    {
        public UpdateBankAccountAsyncTest(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(CreateStripeBankAccountRequest request)
        {
            return await _httpClient.PostAsJsonAsync("api/stripe/bank-account", request);
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
                            var mockStripeAccountService = new Mock<IStripeAccountService>();
                            var mockStripeExternalAccountService = new Mock<IStripeExternalAccountService>();

                            mockStripeReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                            mockStripeAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()))
                                .ThrowsAsync(new StripeException(HttpStatusCode.BadRequest, new StripeError(), string.Empty));

                            container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeService>().SingleInstance();
                            container.RegisterInstance(mockStripeAccountService.Object).As<IStripeAccountService>().SingleInstance();
                            container.RegisterInstance(mockStripeExternalAccountService.Object).As<IStripeExternalAccountService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(
                new CreateStripeBankAccountRequest
                {
                    Token = "123TokenGG"
                });

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
                            var mockStripeAccountService = new Mock<IStripeAccountService>();
                            var mockStripeExternalAccountService = new Mock<IStripeExternalAccountService>();

                            mockStripeReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false);

                            container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeService>().SingleInstance();
                            container.RegisterInstance(mockStripeAccountService.Object).As<IStripeAccountService>().SingleInstance();
                            container.RegisterInstance(mockStripeExternalAccountService.Object).As<IStripeExternalAccountService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(
                new CreateStripeBankAccountRequest
                {
                    Token = "123TokenGG"
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
                            var mockStripeReferenceService = new Mock<IStripeService>();
                            var mockStripeAccountService = new Mock<IStripeAccountService>();
                            var mockStripeExternalAccountService = new Mock<IStripeExternalAccountService>();

                            mockStripeReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                            mockStripeAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()))
                                .ReturnsAsync("acct_123eqwqw12");

                            mockStripeExternalAccountService
                                .Setup(externalAccountService => externalAccountService.ChangeBankAccountAsync(It.IsAny<string>(), It.IsAny<string>()))
                                .ReturnsAsync(
                                    new BankAccount
                                    {
                                        BankName = "BankName",
                                        Country = "CA",
                                        Currency = "CAD",
                                        Last4 = "1234",
                                        Status = "pending",
                                        DefaultForCurrency = true
                                    });

                            container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeService>().SingleInstance();
                            container.RegisterInstance(mockStripeAccountService.Object).As<IStripeAccountService>().SingleInstance();
                            container.RegisterInstance(mockStripeExternalAccountService.Object).As<IStripeExternalAccountService>().SingleInstance();
                        }));

            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync(
                new CreateStripeBankAccountRequest
                {
                    Token = "123TokenGG"
                });

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
