// Filename: UserRepositoryTest.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Cashier.Infrastructure.Tests.Asserts;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Infrastructure.Tests.Repositories
{
    [TestClass]
    public sealed class UserRepositoryTest
    {
        private static readonly UserAggregateFactory UserAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task Create_User_ShouldNotBeEmpty()
        {
            using (var factory = new InMemoryDbContextFactory<CashierDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new UserRepository(context);

                    // Act
                    repository.Create(UserAggregateFactory.CreateUser());

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Assert
                    context.Users.Should().NotBeEmpty();
                }
            }
        }

        [TestMethod]
        public async Task FindAsync_ShouldBeMapped()
        {
            var user = UserAggregateFactory.CreateAdmin();

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
                    var repository = new UserRepository(context);

                    // Act
                    user = await repository.FindAsync(user.Id);

                    // Assert
                    CashierAssert.IsMapped(user);
                }
            }
        }
    }
}