// Filename: TransactionQueriesTest.cs
// Date Created: 2019-05-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Application.Queries;
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.DTO.Factories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Cashier.Tests.Asserts;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Seedwork.Infrastructure.Factories;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Queries
{
    [TestClass]
    public sealed class TransactionQueriesTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private static readonly CashierMapperFactory CashierMapperFactory = CashierMapperFactory.Instance;
        private Mock<ICashierHttpContext> _mockCashierHttpContext;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCashierHttpContext = new Mock<ICashierHttpContext>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<TransactionQueries>.For(typeof(CashierDbContext), typeof(ICashierHttpContext), typeof(IMapper))
                .WithName("TransactionQueries")
                .Assert();
        }

        [TestMethod]
        public async Task GetTransactionsAsync_AccountCurrencyMoney_ShouldBeMapped()
        {
            var userId = FakeCashierFactory.CreateUserId();

            _mockCashierHttpContext.SetupGet(mock => mock.UserId).Returns(userId).Verifiable();

            using (var factory = new InMemoryDbContextFactory<CashierDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new MoneyAccountRepository(context);

                    var account = new MoneyAccount(userId);

                    account.Deposit(Money.Fifty);

                    account.Deposit(Money.OneHundred);

                    account.Deposit(Money.Fifty);

                    repository.Create(account);

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var queries = new TransactionQueries(context, _mockCashierHttpContext.Object, CashierMapperFactory.CreateMapper());

                    // Act
                    var transactions = await queries.GetTransactionsAsync(AccountCurrency.Money);

                    // Assert
                    CashierQueryAssert.IsMapped(transactions);
                }
            }
        }

        [TestMethod]
        public async Task GetTransactionsAsync_AccountCurrencyToken_ShouldBeMapped()
        {
            var userId = FakeCashierFactory.CreateUserId();

            _mockCashierHttpContext.SetupGet(mock => mock.UserId).Returns(userId).Verifiable();

            using (var factory = new InMemoryDbContextFactory<CashierDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new TokenAccountRepository(context);

                    var account = new TokenAccount(userId);

                    account.Deposit(Token.FiftyThousand);

                    account.Deposit(Token.FiveHundredThousand);

                    account.Deposit(Token.FiftyThousand);

                    repository.Create(account);

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var queries = new TransactionQueries(context, _mockCashierHttpContext.Object, CashierMapperFactory.CreateMapper());

                    // Act
                    var transactions = await queries.GetTransactionsAsync(AccountCurrency.Token);

                    // Assert
                    CashierQueryAssert.IsMapped(transactions);
                }
            }
        }
    }
}