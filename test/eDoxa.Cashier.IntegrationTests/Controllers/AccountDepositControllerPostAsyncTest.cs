// Filename: AccountDepositControllerPostAsyncTest.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Requests;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

using ClaimTypes = eDoxa.Seedwork.Security.ClaimTypes;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class AccountDepositControllerPostAsyncTest : IClassFixture<CashierApiFactory>
    {
        public AccountDepositControllerPostAsyncTest(CashierApiFactory factory)
        {
            _factory = factory;
        }

        private readonly CashierApiFactory _factory;

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(AccountDepositPostRequest postRequest)
        {
            return await _httpClient.PostAsync("api/account/deposit", new JsonContent(postRequest));
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var account = new Account(new UserId());

            var factory = _factory.WithClaims(
                new Claim(JwtClaimTypes.Subject, account.UserId.ToString()),
                new Claim(ClaimTypes.StripeCustomerId, "cus_test")
            );

            _httpClient = factory.CreateClient();
            var server = factory.Server;
            server.CleanupDbContext();

            await server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(new AccountDepositPostRequest(Currency.Token, 2500M));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var account = new Account(new UserId());

            var factory = _factory.WithClaims(
                new Claim(JwtClaimTypes.Subject, account.UserId.ToString()),
                new Claim(ClaimTypes.StripeCustomerId, "cus_test")
            );

            _httpClient = factory.CreateClient();
            var server = factory.Server;
            server.CleanupDbContext();

            await server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(new AccountDepositPostRequest(Currency.Money, Money.Fifty));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var message = await response.DeserializeAsync<string>();
            message.Should().NotBeNull();
        }
    }
}
