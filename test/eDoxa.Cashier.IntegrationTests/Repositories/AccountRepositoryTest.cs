// Filename: AccountRepositoryTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Repositories
{
    // TODO: These methods must be refactored into smaller tests.
    // TODO: Avoid using Theory in integration tests.
    public sealed class AccountRepositoryTest : IntegrationTest
    {
        public AccountRepositoryTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        [Fact]
        public async Task AccountScenario()
        {
            var userAccount = new Account(new UserId());

            TestHost.CreateClient();
            var testServer = TestHost.Server;
            testServer.CleanupDbContext();
            
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(userAccount);
                    await accountRepository.CommitAsync();
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindAccountAsync(userAccount.Id);
                    account.Should().NotBeNull();
                    account.Should().Be(account);
                    account?.Transactions.Should().HaveCount(account.Transactions.Count);
                    account?.Transactions.Should().NotContain(transaction => transaction.Status == TransactionStatus.Pending);
                });

            ITransaction moneyDepositTransaction = null;

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindAccountAsync(userAccount.Id);
                    account.Should().NotBeNull();
                    account.Should().Be(userAccount);
                    var moneyAccount = new MoneyAccountDecorator(account);
                    moneyDepositTransaction = moneyAccount.Deposit(Money.Fifty);
                    await accountRepository.CommitAsync();
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindAccountAsync(userAccount.Id);
                    account.Should().NotBeNull();
                    account.Should().Be(userAccount);
                    account?.Transactions.Should().HaveCount(userAccount.Transactions.Count + 1);
                    account?.Transactions.Should().Contain(moneyDepositTransaction);
                    account?.Transactions.Should().ContainSingle(transaction => transaction.Status == TransactionStatus.Pending);
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindAccountAsync(userAccount.Id);
                    account.Should().NotBeNull();
                    account.Should().Be(userAccount);
                    var transaction = account?.Transactions.Single(accountTransaction => accountTransaction.Id == moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction?.MarkAsSucceded();
                    await accountRepository.CommitAsync();
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindAccountAsync(userAccount.Id);
                    account.Should().NotBeNull();
                    account.Should().Be(userAccount);
                    var transaction = account?.Transactions.Single(accountTransaction => accountTransaction.Id == moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    var action = new Action(() => transaction?.MarkAsFailed());
                    action.Should().Throw<InvalidOperationException>();
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindAccountAsync(userAccount.Id);
                    account.Should().NotBeNull();
                    account.Should().Be(userAccount);
                    account?.Transactions.Should().Contain(moneyDepositTransaction);
                    account?.Transactions.Should().ContainSingle(transaction => transaction.Status == TransactionStatus.Succeded);
                });
        }
    }
}
