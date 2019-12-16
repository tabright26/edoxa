// Filename: TransactionRepositoryTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

namespace eDoxa.Cashier.IntegrationTests.Infrastructure.Repositories
{
    // TODO: These methods must be refactored into smaller tests.
    // TODO: Avoid using Theory in integration tests.
    public sealed class TransactionRepositoryTest : IntegrationTest
    {
        public TransactionRepositoryTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        //[Fact]
        //public async Task TransactionScenario_MarkAsSucceded()
        //{
        //    var account = new Account(new UserId());
        //    var moneyAccount = new MoneyAccountDecorator(account);
        //    var depositTransaction = moneyAccount.Deposit(Money.Fifty);

        //    TestHost.CreateClient();
        //    var testServer = TestHost.Server;
        //    testServer.CleanupDbContext();

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<IAccountRepository>();
        //            accountRepository.Create(moneyAccount);
        //            await accountRepository.CommitAsync();
        //        });

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<ITransactionRepository>();
        //            var transaction = await accountRepository.FindTransactionAsync(depositTransaction.Id);
        //            transaction.Should().NotBeNull();
        //            transaction.Should().Be(transaction);
        //            transaction?.Status.Should().Be(TransactionStatus.Pending);
        //        });

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<ITransactionRepository>();
        //            var transaction = await accountRepository.FindTransactionAsync(depositTransaction.Id);
        //            transaction.Should().NotBeNull();
        //            transaction.Should().Be(depositTransaction);
        //            transaction?.MarkAsSucceded();
        //            await accountRepository.CommitAsync();
        //        });

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<ITransactionRepository>();
        //            var transaction = await accountRepository.FindTransactionAsync(depositTransaction.Id);
        //            transaction.Should().NotBeNull();
        //            transaction.Should().Be(depositTransaction);
        //            transaction?.Status.Should().Be(TransactionStatus.Succeded);
        //        });
        //}

        //[Theory]
        //[InlineData(1)]
        //[InlineData(10)]
        //[InlineData(100)]
        //[InlineData(1000)]
        //public async Task TransactionScenario_MarkAsFailed(int seed)
        //{
        //    var account = new Account(new UserId());
        //    var moneyAccount = new MoneyAccountDecorator(account);
        //    var depositTransaction = moneyAccount.Deposit(Money.Fifty);

        //    TestHost.CreateClient();
        //    var testServer = TestHost.Server;
        //    testServer.CleanupDbContext();

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<IAccountRepository>();
        //            accountRepository.Create(moneyAccount);
        //            await accountRepository.CommitAsync();
        //        });

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<ITransactionRepository>();
        //            var transaction = await accountRepository.FindTransactionAsync(depositTransaction.Id);
        //            transaction.Should().NotBeNull();
        //            transaction.Should().Be(depositTransaction);
        //            transaction?.Status.Should().Be(TransactionStatus.Pending);
        //        });

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<ITransactionRepository>();
        //            var transaction = await accountRepository.FindTransactionAsync(depositTransaction.Id);
        //            transaction.Should().NotBeNull();
        //            transaction.Should().Be(depositTransaction);
        //            transaction?.MarkAsFailed();
        //            await accountRepository.CommitAsync();
        //        });

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<ITransactionRepository>();
        //            var transaction = await accountRepository.FindTransactionAsync(depositTransaction.Id);
        //            transaction.Should().NotBeNull();
        //            transaction.Should().Be(depositTransaction);
        //            transaction?.Status.Should().Be(TransactionStatus.Failed);
        //        });
        //}

        //[Fact]
        //public async Task TransactionScenario()
        //{
        //    var transaction1 = new Transaction(
        //        Money.Fifty,
        //        new TransactionDescription("Test"),
        //        TransactionType.Charge,
        //        new UtcNowDateTimeProvider(),
        //        new TransactionMetadata
        //        {
        //            ["ChallengeId"] = new ChallengeId().ToString(),
        //            ["ParticipantId"] = new ParticipantId().ToString()
        //        });

        //    var transaction2 = new Transaction(
        //        Money.Fifty,
        //        new TransactionDescription("Test"),
        //        TransactionType.Charge,
        //        new UtcNowDateTimeProvider(),
        //        new TransactionMetadata
        //        {
        //            ["ChallengeId"] = new ChallengeId().ToString(),
        //            ["ParticipantId"] = new ParticipantId().ToString()
        //        });

        //    var account = new Account(new UserId(), new ITransaction[] { transaction1, transaction2 });

        //    TestHost.CreateClient();
        //    var testServer = TestHost.Server;
        //    testServer.CleanupDbContext();

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<IAccountRepository>();
        //            accountRepository.Create(account);
        //            await accountRepository.CommitAsync();
        //        });

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<ITransactionService>();
        //            var transactionQuery = await accountRepository.FindTransactionAsync(transaction1.Metadata);
        //            transactionQuery.Should().NotBeNull();
        //            transactionQuery.Should().Be(transaction1);
        //            transactionQuery?.Status.Should().Be(TransactionStatus.Pending);
        //        });

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<ITransactionService>();
        //            var transactionQuery = await accountRepository.FindTransactionAsync(transaction1.Metadata);
        //            await accountRepository.MarkTransactionAsSuccededAsync(transactionQuery);
        //            transactionQuery.Status.Should().Be(TransactionStatus.Succeded);
        //        });

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<ITransactionService>();
        //            var transactionQuery = await accountRepository.FindTransactionAsync(transaction1.Metadata);
        //            transactionQuery.Should().NotBeNull();
        //            transactionQuery.Should().Be(transaction1);
        //            transactionQuery?.Status.Should().Be(TransactionStatus.Succeded);
        //        });

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var accountRepository = scope.GetRequiredService<ITransactionService>();
        //            var transactionQuery = await accountRepository.FindTransactionAsync(transaction2.Metadata);
        //            transactionQuery.Should().NotBeNull();
        //            transactionQuery.Should().Be(transaction2);
        //            transactionQuery?.Status.Should().Be(TransactionStatus.Pending);
        //        });
        //}
    }
}
