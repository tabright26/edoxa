// Filename: AccountBalanceControllerGetByCurrencyAsyncTest.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class AccountBalanceControllerGetByCurrencyAsyncTest : IClassFixture<CashierApiFactory>
    {
        public AccountBalanceControllerGetByCurrencyAsyncTest(CashierApiFactory factory)
        {
            _factory = factory;
        }

        private readonly CashierApiFactory _factory;
        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(Currency currency)
        {
            return await _httpClient.GetAsync($"api/account/balance/{currency}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            var accountFaker = new AccountFaker();
            accountFaker.UseSeed(1);
            var account = accountFaker.Generate();
            var factory = _factory.WithClaims(new Claim(JwtClaimTypes.Subject, account.UserId.ToString()));

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
            using var response = await this.ExecuteAsync(Currency.All);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var currency = Currency.Money;
            var accountFaker = new AccountFaker();
            accountFaker.UseSeed(1);
            var account = accountFaker.Generate();
            var balance = account.GetBalanceFor(currency);
            var factory = _factory.WithClaims(new Claim(JwtClaimTypes.Subject, account.UserId.ToString()));
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
            using var response = await this.ExecuteAsync(currency);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var balanceViewModel = await response.DeserializeAsync<BalanceViewModel>();
            balanceViewModel.Should().NotBeNull();
            balanceViewModel?.Currency.Should().Be(currency);
            balanceViewModel?.Available.Should().Be(balance.Available);
            balanceViewModel?.Pending.Should().Be(balance.Pending);
        }
    }
}
