// Filename: TokenAccountRepositoryTest.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Tests.Repositories
{
    [TestClass]
    public sealed class TokenAccountRepositoryTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<TokenAccountRepository>.For(typeof(CashierDbContext)).WithName("TokenAccountRepository").Assert();
        }
    }
}

//        private static readonly UserAggregateFactory UserAggregateFactory = UserAggregateFactory.Instance;

//        [TestMethod]
//        public async Task Create_User_ShouldNotBeEmpty()
//        {
//            using (var factory = new InMemoryDbContextFactory<CashierDbContext>())
//            {
//                using (var context = factory.CreateContext())
//                {
//                    // Arrange
//                    var repository = new UserRepository(context);

//                    // Act
//                    repository.Create(UserAggregateFactory.CreateUser());

//                    await repository.UnitOfWork.CommitAsync();
//                }

//                using (var context = factory.CreateContext())
//                {
//                    // Assert
//                    context.Users.Should().NotBeEmpty();
//                }
//            }
//        }

//        [TestMethod]
//        public async Task FindAsync_ShouldBeMapped()
//        {
//            var user = UserAggregateFactory.CreateAdmin();

//            using (var factory = new InMemoryDbContextFactory<CashierDbContext>())
//            {
//                using (var context = factory.CreateContext())
//                {
//                    var repository = new UserRepository(context);

//                    repository.Create(user);

//                    await repository.UnitOfWork.CommitAsync();
//                }

//                using (var context = factory.CreateContext())
//                {
//                    // Arrange
//                    var repository = new UserRepository(context);

//                    // Act
//                    user = await repository.FindAsync(user.Id);

//                    // Assert
//                    CashierAssert.IsMapped(user);
//                }
//            }
//        }
