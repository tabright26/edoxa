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
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class AccountBalanceControllerGetByCurrencyAsyncTest : IClassFixture<CashierWebApiFactory>
    {
        private readonly CashierWebApiFactory _factory;
        private HttpClient _httpClient;

        public AccountBalanceControllerGetByCurrencyAsyncTest(CashierWebApiFactory factory)
        {
            _factory = factory;
        }

        public static IEnumerable<object[]> ValidCurrencyDataSets => Currency.GetEnumerations().Select(currency => new object[] { currency });

        public static IEnumerable<object[]> InvalidCurrencyDataSets => new[] { new object[] { Currency.All }, new object[] { new Currency() } };

        private async Task<HttpResponseMessage> ExecuteAsync(Currency currency)
        {
            return await _httpClient.GetAsync($"api/account/balance/{currency}");
        }

        [Theory]
        [MemberData(nameof(ValidCurrencyDataSets))]
        public async Task ShouldBeNotValid(Currency currency)
        {
            // Arrange
            var account = new Account(new UserId());

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
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var balanceViewModel = await response.DeserializeAsync<BalanceViewModel>();
            balanceViewModel.Should().NotBeNull();
            balanceViewModel?.Currency.Should().Be(currency);
            balanceViewModel?.Available.Should().Be(decimal.Zero);
            balanceViewModel?.Pending.Should().Be(decimal.Zero);
        }

        [Theory]
        [MemberData(nameof(ValidCurrencyDataSets))]
        public async Task ShouldBeValid(Currency currency)
        {
            // Arrange
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
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var balanceViewModel = await response.DeserializeAsync<BalanceViewModel>();
            balanceViewModel.Should().NotBeNull();
            balanceViewModel?.Currency.Should().Be(currency);
            balanceViewModel?.Available.Should().Be(balance.Available);
            balanceViewModel?.Pending.Should().Be(balance.Pending);
        }

        [Theory]
        [MemberData(nameof(InvalidCurrencyDataSets))]
        public async Task ShouldBeBadRequest(Currency currency)
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
            using var response = await this.ExecuteAsync(currency);

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
