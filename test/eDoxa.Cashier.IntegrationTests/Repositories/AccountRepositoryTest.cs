// Filename: AccountRepositoryTest.cs
// Date Created: 2019-07-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Repositories
{
    //TODO: Changeuh Changeuh Changeuh
    public sealed class AccountRepositoryTest : IClassFixture<CashierWebApiFactory>
    {
        private readonly TestServer _testServer;

        public AccountRepositoryTest(CashierWebApiFactory cashierWebApiFactory)
        {
            cashierWebApiFactory.CreateClient();
            _testServer = cashierWebApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public async Task AccountScenario(int seed)
        {
            var accountFaker = new AccountFaker();
            accountFaker.UseSeed(seed);
            var fakeAccount = accountFaker.Generate();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(fakeAccount);
                    await accountRepository.CommitAsync();
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindUserAccountAsync(fakeAccount.UserId);
                    account.Should().NotBeNull();
                    account.Should().Be(fakeAccount);
                    account?.Transactions.Should().HaveCount(fakeAccount.Transactions.Count);
                    account?.Transactions.Should().NotContain(transaction => transaction.Status == TransactionStatus.Pending);
                }
            );

            var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindUserAccountAsync(fakeAccount.UserId);
                    account.Should().NotBeNull();
                    account.Should().Be(fakeAccount);
                    account?.CreateTransaction(moneyDepositTransaction);
                    await accountRepository.CommitAsync();
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindUserAccountAsync(fakeAccount.UserId);
                    account.Should().NotBeNull();
                    account.Should().Be(fakeAccount);
                    account?.Transactions.Should().HaveCount(fakeAccount.Transactions.Count + 1);
                    account?.Transactions.Should().Contain(moneyDepositTransaction);
                    account?.Transactions.Should().ContainSingle(transaction => transaction.Status == TransactionStatus.Pending);
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindUserAccountAsync(fakeAccount.UserId);
                    account.Should().NotBeNull();
                    account.Should().Be(fakeAccount);
                    var transaction = account?.Transactions.Single(accountTransaction => accountTransaction.Id == moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction?.MarkAsSucceded();
                    await accountRepository.CommitAsync();
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindUserAccountAsync(fakeAccount.UserId);
                    account.Should().NotBeNull();
                    account.Should().Be(fakeAccount);
                    var transaction = account?.Transactions.Single(accountTransaction => accountTransaction.Id == moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction?.MarkAsFailed();
                    await accountRepository.CommitAsync();
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindUserAccountAsync(fakeAccount.UserId);
                    account.Should().NotBeNull();
                    account.Should().Be(fakeAccount);
                    account?.Transactions.Should().Contain(moneyDepositTransaction);
                    account?.Transactions.Should().ContainSingle(transaction => transaction.Status == TransactionStatus.Pending);
                }
            );
        }
    }
}
