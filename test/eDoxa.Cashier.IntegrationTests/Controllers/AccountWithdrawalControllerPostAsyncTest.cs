// Filename: AccountWithdrawalControllerPostAsyncTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Requests;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Controllers
{
    public sealed class AccountWithdrawalControllerPostAsyncTest : IntegrationTest
    {
        public AccountWithdrawalControllerPostAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(CreateTransactionRequest request)
        {
            return await _httpClient.PostAsJsonAsync("api/transactions", request);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeBadRequest()
        {
            // Arrange
            var account = new Account(new UserId());

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, account.UserId.ToString()), new Claim(JwtClaimTypes.Email, "noreply@edoxa.gg"));

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
            using var response = await this.ExecuteAsync(
                new CreateTransactionRequest(
                    "Withdrawal",
                    Currency.Money.Name,
                    Money.Fifty.Amount));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeNotFound()
        {
            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, new UserId().ToString()), new Claim(JwtClaimTypes.Email, "noreply@edoxa.gg"));

            _httpClient = factory.CreateClient();
            var server = factory.Server;
            server.CleanupDbContext();

            // Act
            using var response = await this.ExecuteAsync(
                new CreateTransactionRequest(
                    "Withdrawal",
                    Currency.Money.Name,
                    Money.Fifty.Amount));

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

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, account.UserId.ToString()), new Claim(JwtClaimTypes.Email, "noreply@edoxa.gg"));

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
            using var response = await this.ExecuteAsync(
                new CreateTransactionRequest(
                    "Withdrawal",
                    Currency.Money.Name,
                    Money.Fifty.Amount,
                    new Dictionary<string, string>
                    {
                        ["UserId"] = account.UserId.ToString(),
                        ["Email"] = "noreply@edoxa.gg"
                    }));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var message = await response.Content.ReadAsStringAsync();
            message.Should().NotBeNull();
        }
    }
}
