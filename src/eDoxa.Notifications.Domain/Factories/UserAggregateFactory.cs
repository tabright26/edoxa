// Filename: UserAggregateFactory.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Factories;

namespace eDoxa.Notifications.Domain.Factories
{
    internal sealed partial class UserAggregateFactory : AggregateFactory
    {
        private static readonly Lazy<UserAggregateFactory> Lazy = new Lazy<UserAggregateFactory>(() => new UserAggregateFactory());

        public static UserAggregateFactory Instance
        {
            get
            {
                return Lazy.Value;
            }
        }
    }

    internal sealed partial class UserAggregateFactory
    {
        private readonly NotificationAggregateFactory _notificationAggregateFactory = NotificationAggregateFactory.Instance;

        public User CreateAdmin(bool withNotifications = false)
        {
            var admin = User.Create(AdminData);

            if (withNotifications)
            {
                _notificationAggregateFactory.CreateUserNotifications(admin);
            }

            return admin;
        }

        public User CreateFrancis(bool withNotifications = false)
        {
            var francis = User.Create(FrancisData);

            if (withNotifications)
            {
                _notificationAggregateFactory.CreateUserNotifications(francis);
            }

            return francis;
        }

        public User CreateRoy(bool withNotifications = false)
        {
            var roy = User.Create(RoyData);

            if (withNotifications)
            {
                _notificationAggregateFactory.CreateUserNotifications(roy);
            }

            return roy;
        }

        public User CreateRyan(bool withNotifications = false)
        {
            var ryan = User.Create(RyanData);

            if (withNotifications)
            {
                _notificationAggregateFactory.CreateUserNotifications(ryan);
            }

            return ryan;
        }

        public User CreateUser(bool withNotifications = false)
        {
            var user = User.Create(new UserId());

            if (withNotifications)
            {
                _notificationAggregateFactory.CreateUserNotifications(user);
            }

            return user;
        }

        public IEnumerable<User> CreateUsers(int count)
        {
            for (var index = 0; index < count; index++)
            {
                yield return User.Create(new UserId());
            }
        }
    }
}