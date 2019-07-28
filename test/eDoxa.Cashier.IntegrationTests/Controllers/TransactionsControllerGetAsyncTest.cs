// Filename: TransactionsControllerGetAsyncTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
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
    public sealed class TransactionsControllerGetAsyncTest : IClassFixture<CashierWebApplicationFactory>
    {
        public TransactionsControllerGetAsyncTest(CashierWebApplicationFactory cashierWebApplicationFactory)
        {
            _httpClient = cashierWebApplicationFactory.CreateClient();
            _testServer = cashierWebApplicationFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(
            UserId userId,
            Currency currency = null,
            TransactionType type = null,
            TransactionStatus status = null
        )
        {
            return await _httpClient.DefaultRequestHeaders(new[] {new Claim(JwtClaimTypes.Subject, userId.ToString())})
                .GetAsync($"api/transactions?currency={currency}&type={type}&status={status}");
        }

        [Fact]
        public async Task ShouldBeNoContent()
        {
            // Arrange
            var account = new Account(new UserId());

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync(account.UserId);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task ShouldBeOk()
        {
            // Arrange
            var accountFaker = new AccountFaker();
            accountFaker.UseSeed(1);
            var account = accountFaker.Generate();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync(account.UserId);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var transactions = await response.DeserializeAsync<TransactionViewModel[]>();
            transactions.Should().HaveCount(account.Transactions.Count);
        }
    }
}
