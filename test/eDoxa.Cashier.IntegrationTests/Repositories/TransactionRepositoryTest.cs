// Filename: TransactionRepositoryTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.IntegrationTests.TestHelpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Repositories
{
    // TODO: These methods must be refactored into smaller tests.
    // TODO: Avoid using Theory in integration tests.
    [Collection(nameof(RepositoryCollection))]
    public sealed class TransactionRepositoryTest : RepositoryTest
    {
        public TransactionRepositoryTest(CashierApiFactory apiFactory, TestDataFixture testData) : base(apiFactory, testData)
        {
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public async Task TransactionScenario_MarkAsSucceded(int seed)
        {
            var accountFaker = TestData.AccountFactory.CreateFaker(seed);
            var fakeAccount = accountFaker.FakeAccount();
            var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
            fakeAccount?.CreateTransaction(moneyDepositTransaction);

            ApiFactory.CreateClient();
            var testServer = ApiFactory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(fakeAccount);
                    await accountRepository.CommitAsync();
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Pending);
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.MarkAsSucceded();
                    await accountRepository.CommitAsync();
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Succeded);
                });
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public async Task TransactionScenario_MarkAsFailed(int seed)
        {
            var accountFaker = TestData.AccountFactory.CreateFaker(seed);

            var fakeAccount = accountFaker.FakeAccount();

            var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);

            fakeAccount?.CreateTransaction(moneyDepositTransaction);

            ApiFactory.CreateClient();
            var testServer = ApiFactory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(fakeAccount);
                    await accountRepository.CommitAsync();
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Pending);
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.MarkAsFailed();
                    await accountRepository.CommitAsync();
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<ITransactionRepository>();
                    var transaction = await accountRepository.FindTransactionAsync(moneyDepositTransaction.Id);
                    transaction.Should().NotBeNull();
                    transaction.Should().Be(moneyDepositTransaction);
                    transaction?.Status.Should().Be(TransactionStatus.Failed);
                });
        }
    }
}
