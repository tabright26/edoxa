// Filename: TransactionRepositoryTest.cs
// Date Created: 2019-07-04
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
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Transactions;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.IntegrationTests.Helpers;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    public sealed class TransactionRepositoryTest
    {
        private TestServer _testServer;

        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new WebApplicationFactory<TestStartup>();
            factory.CreateClient();
            _testServer = factory.Server;
            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var context = scope.GetService<CashierDbContext>();
                    context.Accounts.RemoveRange(context.Accounts);
                    await context.SaveChangesAsync();
                }
            );
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        public async Task TransactionScenario_MarkAsSucceded(int seed)
        {
            var accountFaker = new AccountFaker();
            accountFaker.UseSeed(seed);
            var fakeAccount = accountFaker.Generate();
            var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
            fakeAccount?.CreateTransaction(moneyDepositTransaction);

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
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Pending);
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.MarkAsSucceded();
                    await accountRepository.CommitAsync();
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Succeded);
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.MarkAsFailed();
                    await accountRepository.CommitAsync();
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Succeded);
                }
            );
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(10)]
        [DataRow(100)]
        [DataRow(1000)]
        public async Task TransactionScenario_MarkAsFailed(int seed)
        {
            var accountFaker = new AccountFaker();
            accountFaker.UseSeed(seed);
            var fakeAccount = accountFaker.Generate();
            var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
            fakeAccount?.CreateTransaction(moneyDepositTransaction);

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
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Pending);
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.MarkAsFailed();
                    await accountRepository.CommitAsync();
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Failed);
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.MarkAsSucceded();
                    await accountRepository.CommitAsync();
                }
            );

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Failed);
                }
            );
        }
    }
}
