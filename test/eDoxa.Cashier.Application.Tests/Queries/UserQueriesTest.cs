// Filename: UserQueriesTest.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Application.Queries;
using eDoxa.Cashier.Application.Tests.Asserts;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.DTO.Factories;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Seedwork.Infrastructure.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Application.Tests.Queries
{
    [TestClass]
    public sealed class UserQueriesTest
    {
        private readonly CashierMapperFactory _cashierMapperFactory = CashierMapperFactory.Instance;
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task FindUserAccountAsync_ShouldBeMapped()
        {
            var user = _userAggregateFactory.CreateUser();

            using (var factory = new InMemoryDbContextFactory<CashierDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new UserRepository(context);

                    repository.Create(user);

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var queries = new MoneyAccountQueries(context, _cashierMapperFactory.CreateMapper());

                    // Act
                    var accountDTO = await queries.FindAccountAsync(user.Id);

                    // Assert
                    CashierAssert.IsMapped(accountDTO);
                }
            }
        }
    }
}