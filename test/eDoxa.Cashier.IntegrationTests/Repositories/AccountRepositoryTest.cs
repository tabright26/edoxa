﻿// Filename: AccountRepositoryTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
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

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public async Task AccountScenario(int seed)
        {
            var accountFaker = TestData.FakerFactory.CreateAccountFaker(seed);

            var fakeAccount = accountFaker.FakeAccount();

            TestHost.CreateClient();
            var testServer = TestHost.Server;
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
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindUserAccountAsync(fakeAccount.UserId);
                    account.Should().NotBeNull();
                    account.Should().Be(fakeAccount);
                    account?.Transactions.Should().HaveCount(fakeAccount.Transactions.Count);
                    account?.Transactions.Should().NotContain(transaction => transaction.Status == TransactionStatus.Pending);
                });

            var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindUserAccountAsync(fakeAccount.UserId);
                    account.Should().NotBeNull();
                    account.Should().Be(fakeAccount);
                    account?.CreateTransaction(moneyDepositTransaction);
                    await accountRepository.CommitAsync();
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindUserAccountAsync(fakeAccount.UserId);
                    account.Should().NotBeNull();
                    account.Should().Be(fakeAccount);
                    account?.Transactions.Should().HaveCount(fakeAccount.Transactions.Count + 1);
                    account?.Transactions.Should().Contain(moneyDepositTransaction);
                    account?.Transactions.Should().ContainSingle(transaction => transaction.Status == TransactionStatus.Pending);
                });

            await testServer.UsingScopeAsync(
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
                });

            await testServer.UsingScopeAsync(
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
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    var account = await accountRepository.FindUserAccountAsync(fakeAccount.UserId);
                    account.Should().NotBeNull();
                    account.Should().Be(fakeAccount);
                    account?.Transactions.Should().Contain(moneyDepositTransaction);
                    account?.Transactions.Should().ContainSingle(transaction => transaction.Status == TransactionStatus.Pending);
                });
        }
    }
}
