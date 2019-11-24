// Filename: TransactionRepositoryTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Transactions.Services.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.Repositories
{
    // TODO: These methods must be refactored into smaller tests.
    // TODO: Avoid using Theory in integration tests.
    public sealed class TransactionRepositoryTest : IntegrationTest
    {
        public TransactionRepositoryTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public async Task TransactionScenario_MarkAsSucceded(int seed)
        {
            var accountFaker = TestData.FakerFactory.CreateAccountFaker(seed);
            var fakeAccount = accountFaker.FakeAccount();
            var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);
            fakeAccount?.CreateTransaction(moneyDepositTransaction);

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
            var accountFaker = TestData.FakerFactory.CreateAccountFaker(seed);

            var fakeAccount = accountFaker.FakeAccount();

            var moneyDepositTransaction = new MoneyDepositTransaction(Money.Fifty);

            fakeAccount?.CreateTransaction(moneyDepositTransaction);

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

        [Fact]
        public async Task TransactionScenario()
        {
            var account = new Account(new UserId());

            var transaction1 = new Transaction(
                Money.Fifty,
                new TransactionDescription("Test"),
                TransactionType.Charge,
                new UtcNowDateTimeProvider(),
                new TransactionMetadata
                {
                    ["ChallengeId"] = new ChallengeId().ToString(),
                    ["ParticipantId"] = new ParticipantId().ToString()
                });

            var transaction2 = new Transaction(
                Money.Fifty,
                new TransactionDescription("Test"),
                TransactionType.Charge,
                new UtcNowDateTimeProvider(),
                new TransactionMetadata
                {
                    ["ChallengeId"] = new ChallengeId().ToString(),
                    ["ParticipantId"] = new ParticipantId().ToString()
                });

            account.CreateTransaction(transaction1);

            account.CreateTransaction(transaction2);

            TestHost.CreateClient();
            var testServer = TestHost.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<IAccountRepository>();
                    accountRepository.Create(account);
                    await accountRepository.CommitAsync();
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<ITransactionService>();
                    var transactionQuery = await accountRepository.FindTransactionAsync(transaction1.Metadata);
                    transactionQuery.Should().NotBeNull();
                    transactionQuery.Should().Be(transaction1);
                    transactionQuery?.Status.Should().Be(TransactionStatus.Pending);
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<ITransactionService>();
                    var transactionQuery = await accountRepository.FindTransactionAsync(transaction1.Metadata);
                    await accountRepository.MarkTransactionAsSuccededAsync(transactionQuery);
                    transactionQuery.Status.Should().Be(TransactionStatus.Succeded);
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<ITransactionService>();
                    var transactionQuery = await accountRepository.FindTransactionAsync(transaction1.Metadata);
                    transactionQuery.Should().NotBeNull();
                    transactionQuery.Should().Be(transaction1);
                    transactionQuery?.Status.Should().Be(TransactionStatus.Succeded);
                });

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var accountRepository = scope.GetRequiredService<ITransactionService>();
                    var transactionQuery = await accountRepository.FindTransactionAsync(transaction2.Metadata);
                    transactionQuery.Should().NotBeNull();
                    transactionQuery.Should().Be(transaction2);
                    transactionQuery?.Status.Should().Be(TransactionStatus.Pending);
                });
        }
    }
}
