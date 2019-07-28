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
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Helpers;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class AccountWithdrawalControllerPostAsyncTest : IClassFixture<CashierWebApplicationFactory>
    {
        public AccountWithdrawalControllerPostAsyncTest(CashierWebApplicationFactory cashierWebApplicationFactory)
        {
            _httpClient = cashierWebApplicationFactory.CreateClient();
            _testServer = cashierWebApplicationFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(UserId userId, string connectAccountId, WithdrawalRequest request)
        {
            return await _httpClient
                .DefaultRequestHeaders(
                    new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(CustomClaimTypes.StripeConnectAccountId, connectAccountId)}
                )
                .PostAsync("api/account/withdrawal", new JsonContent(request));
        }

        [Fact]
        public async Task Money_InsufficientFunds_ShouldBeStatus400BadRequest()
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
            var response = await this.ExecuteAsync(account.UserId, "acct_test", new WithdrawalRequest(Money.Fifty));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task Money_InvalidAmount_ShouldBeStatus400BadRequest()
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
            var response = await this.ExecuteAsync(account.UserId, "acct_test", new WithdrawalRequest(2.5M));

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

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var transactionRepository = scope.GetService<ITransactionRepository>();
                    transaction = await transactionRepository.FindTransactionAsync(transaction.Id);
                    transaction?.MarkAsSucceded();
                    await transactionRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync(account.UserId, "acct_test", new WithdrawalRequest(Money.Fifty));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var message = await response.DeserializeAsync<string>();
            message.Should().NotBeNull();
        }

        [Fact]
        public async Task User_WithoutAccount_ShouldBeStatus404NotFound()
        {
            // Act
            var response = await this.ExecuteAsync(new UserId(), "acct_test", new WithdrawalRequest(Money.Fifty));

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
