// Filename: TransactionRepositoryTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    public sealed class TransactionRepositoryTest : CashierWebApplicationFactory
    {
        [TestInitialize]
        public async Task TestInitialize()
        {
            this.CreateClient();

            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await Server.CleanupDbContextAsync();
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

            await Server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<IAccountRepository>();
                    accountRepository.Create(fakeAccount);
                    await accountRepository.CommitAsync();
                }
            );

            await Server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Pending);
                }
            );

            await Server.UsingScopeAsync(
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

            await Server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Succeded);
                }
            );

            await Server.UsingScopeAsync(
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

            await Server.UsingScopeAsync(
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

            await Server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<IAccountRepository>();
                    accountRepository.Create(fakeAccount);
                    await accountRepository.CommitAsync();
                }
            );

            await Server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Pending);
                }
            );

            await Server.UsingScopeAsync(
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

            await Server.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Failed);
                }
            );

            await Server.UsingScopeAsync(
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

            await Server.UsingScopeAsync(
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
