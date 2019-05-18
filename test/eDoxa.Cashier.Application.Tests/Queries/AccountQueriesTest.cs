// Filename: AccountQueriesTest.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Application.Queries;
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.DTO.Factories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Cashier.Tests.Asserts;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Seedwork.Infrastructure.Factories;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Queries
{
    [TestClass]
    public sealed class AccountQueriesTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private static readonly CashierMapperFactory CashierMapperFactory = CashierMapperFactory.Instance;
        private Mock<ICashierSecurity> _mockCashierSecurity;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCashierSecurity = new Mock<ICashierSecurity>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<AccountQueries>.For(typeof(CashierDbContext), typeof(ICashierSecurity), typeof(IMapper))
                .WithName("AccountQueries")
                .Assert();
        }

        [TestMethod]
        public async Task GetAccountAsync_AccountCurrencyMoney_ShouldBeMapped()
        {
            var userId = FakeCashierFactory.CreateUserId();

            _mockCashierSecurity.SetupGet(mock => mock.UserId).Returns(userId).Verifiable();

            using (var factory = new InMemoryDbContextFactory<CashierDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new MoneyAccountRepository(context);

                    repository.Create(new MoneyAccount(userId));

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var queries = new AccountQueries(context, _mockCashierSecurity.Object, CashierMapperFactory.CreateMapper());

                    // Act
                    var account = await queries.GetAccountAsync(AccountCurrency.Money);

                    // Assert
                    CashierQueryAssert.IsMapped(account.Single());
                }
            }
        }

        [TestMethod]
        public async Task GetAccountAsync_AccountCurrencyToken_ShouldBeMapped()
        {
            var userId = FakeCashierFactory.CreateUserId();

            _mockCashierSecurity.SetupGet(mock => mock.UserId).Returns(userId).Verifiable();

            using (var factory = new InMemoryDbContextFactory<CashierDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new TokenAccountRepository(context);

                    repository.Create(new TokenAccount(userId));

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var queries = new AccountQueries(context, _mockCashierSecurity.Object, CashierMapperFactory.CreateMapper());

                    // Act
                    var account = await queries.GetAccountAsync(AccountCurrency.Token);

                    // Assert
                    CashierQueryAssert.IsMapped(account.Single());
                }
            }
        }
    }
}