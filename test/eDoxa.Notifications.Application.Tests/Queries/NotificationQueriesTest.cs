// Filename: NotificationQueriesTest.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Notifications.Application.Queries;
using eDoxa.Notifications.Application.Tests.Asserts;
using eDoxa.Notifications.Domain.Factories;
using eDoxa.Notifications.DTO.Factories;
using eDoxa.Notifications.Infrastructure;
using eDoxa.Notifications.Infrastructure.Repositories;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Notifications.Application.Tests.Queries
{
    [TestClass]
    public sealed class NotificationQueriesTest
    {
        private readonly NotificationsMapperFactory _notificationsMapperFactory = NotificationsMapperFactory.Instance;
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task FindUserNotificationsAsync_ShouldBeMapped()
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
                    var queries = new NotificationQueries(context, _notificationsMapperFactory.CreateMapper());

                    // Act
                    var notificationListDTO = await queries.FindUserNotificationsAsync(user.Id);

                    // Assert
                    NotificationsAssert.IsMapped(notificationListDTO);
                }
            }
        }

        [TestMethod]
        public async Task FindUserNotificationsAsync_NullReference_ShouldBeEmpty()
        {
            using (var factory = new InMemoryDbContextFactory<NotificationsDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var queries = new NotificationQueries(context, _notificationsMapperFactory.CreateMapper());

                    // Act
                    var notificationListDTO = await queries.FindUserNotificationsAsync(null);

                    // Assert
                    notificationListDTO.Items.Should().BeEmpty();
                }
            }
        }

        [TestMethod]
        public async Task FindUserNotificationAsync_ShouldBeMapped()
        {
            var user = _userAggregateFactory.CreateUser(true);

            var notification = user.Notifications.First();

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
                    var queries = new NotificationQueries(context, _notificationsMapperFactory.CreateMapper());

                    // Act
                    var notificationDTO = await queries.FindUserNotificationAsync(user.Id, notification.Id);

                    // Assert
                    NotificationsAssert.IsMapped(notificationDTO);
                }
            }
        }

        [TestMethod]
        public async Task FindUserNotificationAsync_NullReference_ShouldBeNull()
        {
            using (var factory = new InMemoryDbContextFactory<NotificationsDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var queries = new NotificationQueries(context, _notificationsMapperFactory.CreateMapper());

                    // Act
                    var notificationDTO = await queries.FindUserNotificationAsync(null, null);

                    // Assert
                    notificationDTO.Should().BeNull();
                }
            }
        }
    }
}