// Filename: AccountBalanceControllerGetByCurrencyAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Responses;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class AccountBalanceControllerGetByCurrencyAsyncTest : IntegrationTestClass
    {
        public AccountBalanceControllerGetByCurrencyAsyncTest(TestApiFactory testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(Currency currency)
        {
            return await _httpClient.GetAsync($"api/account/balance/{currency}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeInternalServerError()
        {
            var accountFaker = TestData.FakerFactory.CreateAccountFaker(1);

            var account = accountFaker.FakeAccount();

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, account.UserId.ToString()));

            _httpClient = factory.CreateClient();
            var server = factory.Server;
            server.CleanupDbContext();

            await server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(Currency.All);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var currency = Currency.Money;
            var accountFaker = TestData.FakerFactory.CreateAccountFaker(1);
            var account = accountFaker.FakeAccount();
            var balance = account.GetBalanceFor(currency);
            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, account.UserId.ToString()));
            _httpClient = factory.CreateClient();
            var server = factory.Server;
            server.CleanupDbContext();

            await server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(currency);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var balanceResponse = await response.DeserializeAsync<BalanceResponse>();
            balanceResponse.Should().NotBeNull();
            balanceResponse?.Currency.Should().Be(currency);
            balanceResponse?.Available.Should().Be(balance.Available);
            balanceResponse?.Pending.Should().Be(balance.Pending);
        }
    }
}
