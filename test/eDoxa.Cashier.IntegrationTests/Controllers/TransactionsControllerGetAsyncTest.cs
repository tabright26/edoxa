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

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class TransactionsControllerGetAsyncTest : IClassFixture<CashierWebApiFactory>
    {
        private readonly CashierWebApiFactory _factory;

        public TransactionsControllerGetAsyncTest(CashierWebApiFactory factory)
        {
            _factory = factory;
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(
            Currency currency = null,
            TransactionType type = null,
            TransactionStatus status = null
        )
        {
            return await _httpClient.GetAsync($"api/transactions?currency={currency}&type={type}&status={status}");
        }

        [Fact]
        public async Task ShouldBeNoContent()
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
            using var response = await this.ExecuteAsync();

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
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var transactions = await response.DeserializeAsync<TransactionViewModel[]>();
            transactions.Should().HaveCount(account.Transactions.Count);
        }
    }
}
