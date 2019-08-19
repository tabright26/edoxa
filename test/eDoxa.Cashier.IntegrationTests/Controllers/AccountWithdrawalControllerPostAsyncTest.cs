// Filename: AccountWithdrawalControllerPostAsyncTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Requests;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class AccountWithdrawalControllerPostAsyncTest : IClassFixture<CashierWebApiFactory>
    {
        public AccountWithdrawalControllerPostAsyncTest(CashierWebApiFactory factory)
        {
            _factory = factory;
        }

        private readonly CashierWebApiFactory _factory;

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(WithdrawalRequest request)
        {
            return await _httpClient.PostAsync("api/account/withdrawal", new JsonContent(request));
        }

        [Fact]
        public async Task Money_InsufficientFunds_ShouldBeStatus400BadRequest()
        {
            // Arrange
            var account = new Account(new UserId());

            var factory = _factory.WithClaims(
                new Claim(JwtClaimTypes.Subject, account.UserId.ToString()),
                new Claim(AppClaimTypes.StripeConnectAccountId, "acct_test")
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
            using var response = await this.ExecuteAsync(new WithdrawalRequest(Money.Fifty));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task Money_InvalidAmount_ShouldBeStatus400BadRequest()
        {
            // Arrange
            var account = new Account(new UserId());

            var factory = _factory.WithClaims(
                new Claim(JwtClaimTypes.Subject, account.UserId.ToString()),
                new Claim(AppClaimTypes.StripeConnectAccountId, "acct_test")
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
            using var response = await this.ExecuteAsync(new WithdrawalRequest(2.5M));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task Money_ValidAmount_ShouldBeStatus200OK()
        {
            // Arrange
            var account = new Account(new UserId());

            ITransaction transaction = new MoneyDepositTransaction(Money.Fifty);

            account.CreateTransaction(transaction);

            var factory = _factory.WithClaims(
                new Claim(JwtClaimTypes.Subject, account.UserId.ToString()),
                new Claim(AppClaimTypes.StripeConnectAccountId, "acct_test")
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

            await server.UsingScopeAsync(
                async scope =>
                {
                    var transactionRepository = scope.GetRequiredService<ITransactionRepository>();
                    transaction = await transactionRepository.FindTransactionAsync(transaction.Id);
                    transaction?.MarkAsSucceded();
                    await transactionRepository.CommitAsync();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(new WithdrawalRequest(Money.Fifty));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var message = await response.DeserializeAsync<string>();
            message.Should().NotBeNull();
        }

        [Fact]
        public async Task User_WithoutAccount_ShouldBeStatus404NotFound()
        {
            var factory = _factory.WithClaims(
                new Claim(JwtClaimTypes.Subject,new UserId().ToString()),
                new Claim(AppClaimTypes.StripeConnectAccountId, "acct_test")
            );

            _httpClient = factory.CreateClient();
            var server = factory.Server;
            server.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(new WithdrawalRequest(Money.Fifty));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
