// Filename: AccountBalanceControllerGetByCurrencyAsyncTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class AccountBalanceControllerGetByCurrencyAsyncTest : IClassFixture<CashierWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        public AccountBalanceControllerGetByCurrencyAsyncTest(CashierWebApplicationFactory cashierWebApplicationFactory)
        {
            _httpClient = cashierWebApplicationFactory.CreateClient();
            _testServer = cashierWebApplicationFactory.Server;
            _testServer.CleanupDbContext();
        }

        public static IEnumerable<object[]> ValidCurrencyDataSets => Currency.GetEnumerations().Select(currency => new object[] { currency }).ToList();

        public static IEnumerable<object[]> InvalidCurrencyDataSets => new[] { new object[] { Currency.All }, new object[] { new Currency() } }.ToList();

        private async Task<HttpResponseMessage> ExecuteAsync(UserId userId, Currency currency)
        {
            return await _httpClient.DefaultRequestHeaders(new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()) })
                .GetAsync($"api/account/balance/{currency}");
        }

        [Theory]
        [MemberData(nameof(ValidCurrencyDataSets))]
        public async Task ShouldHaveNoAvailableFundsAndNoPendingFunds(Currency currency)
        {
            // Arrange
            var account = new Account(new UserId());

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync(account.UserId, currency);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var balanceViewModel = await response.DeserializeAsync<BalanceViewModel>();
            balanceViewModel.Should().NotBeNull();
            balanceViewModel?.Currency.Should().Be(currency);
            balanceViewModel?.Available.Should().Be(decimal.Zero);
            balanceViewModel?.Pending.Should().Be(decimal.Zero);
        }

        [Theory]
        [MemberData(nameof(ValidCurrencyDataSets))]
        public async Task ShouldHaveAvailableFundsAndPendingFunds(Currency currency)
        {
            // Arrange
            var accountFaker = new AccountFaker();
            accountFaker.UseSeed(1);
            var account = accountFaker.Generate();
            var balance = account.GetBalanceFor(currency);

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync(account.UserId, currency);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var balanceViewModel = await response.DeserializeAsync<BalanceViewModel>();
            balanceViewModel.Should().NotBeNull();
            balanceViewModel?.Currency.Should().Be(currency);
            balanceViewModel?.Available.Should().Be(balance.Available);
            balanceViewModel?.Pending.Should().Be(balance.Pending);
        }

        [Theory]
        [MemberData(nameof(ValidCurrencyDataSets))]
        public async Task UserWithoutAccount_ShouldBeNotFound(Currency currency)
        {
            // Act
            var response = await this.ExecuteAsync(new UserId(), currency);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Theory]
        [MemberData(nameof(InvalidCurrencyDataSets))]
        public async Task InvalidCurrency_ShouldBeBadRequest(Currency currency)
        {
            var accountFaker = new AccountFaker();
            accountFaker.UseSeed(1);
            var account = accountFaker.Generate();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync(account.UserId, currency);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
