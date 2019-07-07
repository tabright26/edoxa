// Filename: AccountRepositoryTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Fakers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    public sealed class AccountRepositoryTest
    {
        private TestServer _testServer;

        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new TestCashieWebApplicationFactory<TestCashierStartup>();
            factory.CreateClient();
            _testServer = factory.Server;
            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _testServer.CleanupDbContextAsync();
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        public async Task AccountScenario(int seed)
        {
            var accountFaker = new AccountFaker();
            accountFaker.UseSeed(seed);
            var fakeAccount = accountFaker.Generate();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<IAccountRepository>();
                    accountRepository.Create(fakeAccount);
                    await accountRepository.CommitAsync();
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<IAccountRepository>();
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
                    var accountRepository = scope.GetService<IAccountRepository>();
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
                    var accountRepository = scope.GetService<IAccountRepository>();
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
                    var accountRepository = scope.GetService<IAccountRepository>();
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
                    var accountRepository = scope.GetService<IAccountRepository>();
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
                    var accountRepository = scope.GetService<IAccountRepository>();
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
