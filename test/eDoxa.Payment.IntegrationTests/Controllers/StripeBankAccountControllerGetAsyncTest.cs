﻿// Filename: BankAccountControllerGetAsyncTest.cs
// Date Created: 2019-10-11
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Autofac;

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

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Payment.IntegrationTests.Controllers
{
    public sealed class StripeBankAccountControllerGetAsyncTest : IntegrationTest
    {
        public StripeBankAccountControllerGetAsyncTest(TestHostFixture testHost, TestMapperFixture testMapper) : base(testHost, testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync($"api/stripe/bank-account");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOk()
        {
            // Arrange
            var userId = new UserId();
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(builder => builder.ConfigureTestContainer<ContainerBuilder>(
                container =>
                {
                    var mockStripeReferenceService = new Mock<IStripeService>();
                    var mockStripeAccountService = new Mock<IStripeAccountService>();
                    var mockStripeExternalAccountService = new Mock<IStripeExternalAccountService>();

                    mockStripeReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                    mockStripeAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("acct_123eqwqw12");

                    mockStripeExternalAccountService.Setup(externalAccountService => externalAccountService.FindBankAccountAsync(It.IsAny<string>()))
                        .ReturnsAsync(new BankAccount
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
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(builder => builder.ConfigureTestContainer<ContainerBuilder>(
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
            using var response = await this.ExecuteAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var userId = new UserId();
            var factory = TestHost.WithClaimsFromDefaultAuthentication(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(builder => builder.ConfigureTestContainer<ContainerBuilder>(
                container =>
                {
                    var mockStripeReferenceService = new Mock<IStripeService>();
                    var mockStripeAccountService = new Mock<IStripeAccountService>();
                    var mockStripeExternalAccountService = new Mock<IStripeExternalAccountService>();

                    mockStripeReferenceService.Setup(referenceService => referenceService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

                    mockStripeAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ThrowsAsync(new StripeException(HttpStatusCode.BadRequest, new StripeError(), string.Empty));

                    container.RegisterInstance(mockStripeReferenceService.Object).As<IStripeService>().SingleInstance();
                    container.RegisterInstance(mockStripeAccountService.Object).As<IStripeAccountService>().SingleInstance();
                    container.RegisterInstance(mockStripeExternalAccountService.Object).As<IStripeExternalAccountService>().SingleInstance();
                }));
            _httpClient = factory.CreateClient();

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}