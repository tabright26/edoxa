// Filename: NotificationRepositoryTest.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Notifications.Domain.Factories;
using eDoxa.Notifications.Infrastructure.Repositories;
using eDoxa.Notifications.Infrastructure.Tests.Asserts;
using eDoxa.Seedwork.Infrastructure;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Notifications.Infrastructure.Tests.Repositories
{
    [TestClass]
    public sealed class NotificationRepositoryTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task Delete_Notification_ShouldBeNull()
        {
            var user = _userAggregateFactory.CreateUser(true);

            var notification = user.Notifications.First();

            using (var factory = new CustomDbContextFactory<NotificationsDbContext>())
            {
                // Arrange
                using (var context = factory.CreateContext())
                {
                    var repository = new UserRepository(context);

                    repository.Create(user);

                    await repository.UnitOfWork.CommitAsync();
                }

                // Act
                using (var context = factory.CreateContext())
                {
                    var repository = new NotificationRepository(context);

                    repository.Delete(notification);

                    await repository.UnitOfWork.CommitAsync();
                }

                // Assert
                using (var context = factory.CreateContext())
                {
                    var repository = new NotificationRepository(context);

                    notification = await repository.FindAsync(notification.Id);

                    notification.Should().BeNull();
                }
            }
        }

        [TestMethod]
        public void Delete_NullReference_ShouldThrowArgumentNullException()
        {
            using (var factory = new CustomDbContextFactory<NotificationsDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new NotificationRepository(context);

                    // Act
                    var action = new Action(() => repository.Delete(null));

                    // Assert
                    action.Should().Throw<ArgumentNullException>();
                }
            }
        }

        [TestMethod]
        public async Task FindAsync_ShouldBeMapped()
        {
            var user = _userAggregateFactory.CreateUser(true);

            var notification = user.Notifications.First();

            using (var factory = new CustomDbContextFactory<NotificationsDbContext>())
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
                    var repository = new NotificationRepository(context);

                    // Act
                    notification = await repository.FindAsync(notification.Id);

                    // Assert
                    NotificationsAssert.IsMapped(notification);
                }
            }
        }

        [TestMethod]
        public async Task FindAsync_NullReference_ShouldBeNull()
        {
            using (var factory = new CustomDbContextFactory<NotificationsDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new NotificationRepository(context);

                    // Act
                    var notification = await repository.FindAsync(null);

                    // Assert
                    notification.Should().BeNull();
                }
            }
        }
    }
}