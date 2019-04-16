// Filename: UserRepositoryTest.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Notifications.Domain.Factories;
using eDoxa.Notifications.Infrastructure.Repositories;
using eDoxa.Notifications.Infrastructure.Tests.Asserts;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Notifications.Infrastructure.Tests.Repositories
{
    [TestClass]
    public sealed class UserRepositoryTest
    {
        private static readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task Create_User_ShouldNotBeEmpty()
        {
            using (var factory = new InMemoryDbContextFactory<NotificationsDbContext>())
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
        public async Task FindAsync_ShouldBeMapped()
        {
            var user = _userAggregateFactory.CreateUser(true);

            using (var factory = new InMemoryDbContextFactory<NotificationsDbContext>())
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
                    NotificationsAssert.IsMapped(user);
                }
            }
        }
    }
}