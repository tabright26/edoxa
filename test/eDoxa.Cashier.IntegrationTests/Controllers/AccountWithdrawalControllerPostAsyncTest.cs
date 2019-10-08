// Filename: AccountWithdrawalControllerPostAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelpers;
using eDoxa.Cashier.TestHelpers.Fixtures;
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
    public sealed class AccountWithdrawalControllerPostAsyncTest : IntegrationTest
    {
        public AccountWithdrawalControllerPostAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(Currency currency, decimal amount)
        {
            return await _httpClient.PostAsync($"api/account/withdrawal/{currency}", new JsonContent(amount));
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var account = new Account(new UserId());

            var factory = TestApi.WithClaims(
                new Claim(JwtClaimTypes.Subject, account.UserId.ToString()),
                new Claim(JwtClaimTypes.Email, "noreply@edoxa.gg"),
                new Claim(ClaimTypes.StripeConnectAccountId, "acct_test"));

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
            using var response = await this.ExecuteAsync(Currency.Money, Money.Fifty);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            var factory = TestApi.WithClaims(
                new Claim(JwtClaimTypes.Subject, new UserId().ToString()),
                new Claim(JwtClaimTypes.Email, "noreply@edoxa.gg"),
                new Claim(ClaimTypes.StripeConnectAccountId, "acct_test"));

            _httpClient = factory.CreateClient();
            var server = factory.Server;
            server.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(Currency.Money, Money.Fifty);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var account = new Account(new UserId());

            ITransaction transaction = new MoneyDepositTransaction(Money.Fifty);

            account.CreateTransaction(transaction);

            var factory = TestApi.WithClaims(
                new Claim(JwtClaimTypes.Subject, account.UserId.ToString()),
                new Claim(JwtClaimTypes.Email, "noreply@edoxa.gg"),
                new Claim(ClaimTypes.StripeConnectAccountId, "acct_test"));

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

            await server.UsingScopeAsync(
                async scope =>
                {
                    var transactionRepository = scope.GetRequiredService<ITransactionRepository>();
                    transaction = await transactionRepository.FindTransactionAsync(transaction.Id);
                    transaction?.MarkAsSucceded();
                    await transactionRepository.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(Currency.Money, Money.Fifty);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var message = await response.DeserializeAsync<string>();
            message.Should().NotBeNull();
        }
    }
}
