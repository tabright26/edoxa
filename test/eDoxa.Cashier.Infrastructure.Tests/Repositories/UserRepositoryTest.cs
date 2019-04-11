﻿// Filename: UserRepositoryTest.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Cashier.Infrastructure.Tests.Asserts;
using eDoxa.Seedwork.Infrastructure;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Infrastructure.Tests.Repositories
{
    [TestClass]
    public sealed class UserRepositoryTest
    {
        private static readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task Create_User_ShouldNotBeEmpty()
        {
            using (var factory = new CustomDbContextFactory<CashierDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new UserRepository(context);

                    // Act
                    repository.Create(_userAggregateFactory.CreateUser());

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
        public void Create_UserNullReference_ShouldThrowArgumentNullException()
        {
            using (var factory = new CustomDbContextFactory<CashierDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new UserRepository(context);

                    // Act
                    var action = new Action(() => repository.Create(null));

                    // Assert
                    action.Should().Throw<ArgumentNullException>();
                }
            }
        }

        [TestMethod]
        public async Task FindAsync_ShouldBeMapped()
        {
            var user = _userAggregateFactory.CreateUser();

            using (var factory = new CustomDbContextFactory<CashierDbContext>())
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

        [TestMethod]
        public async Task FindAsync_NullReference_ShouldBeNull()
        {
            using (var factory = new CustomDbContextFactory<CashierDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new UserRepository(context);

                    // Act
                    var user = await repository.FindAsync(null);

                    // Assert
                    user.Should().BeNull();
                }
            }
        }
    }
}