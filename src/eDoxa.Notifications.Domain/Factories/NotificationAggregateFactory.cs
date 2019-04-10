// Filename: NotificationAggregateFactory.cs
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
using System.Linq;

using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Factories;

namespace eDoxa.Notifications.Domain.Factories
{
    internal sealed partial class NotificationAggregateFactory : AggregateFactory
    {
        private static readonly Lazy<NotificationAggregateFactory> _lazy = new Lazy<NotificationAggregateFactory>(() => new NotificationAggregateFactory());

        public static NotificationAggregateFactory Instance
        {
            get
            {
                return _lazy.Value;
            }
        }
    }

    internal sealed partial class NotificationAggregateFactory
    {
        // NotificationNames.cs
        public const string NotificationName = "user.email.updated";

        // NotificationDescription.cs
        public const string NotificationDescriptionName = NotificationName;
        public const string NotificationDescriptionTitle = nameof(NotificationDescriptionTitle);
        public const string NotificationDescriptionTemplate = nameof(NotificationDescriptionTemplate);
    }

    internal sealed partial class NotificationAggregateFactory
    {
        public void CreateUserNotifications(User admin)
        {
            foreach (var name in NotificationNames.GetValues())
            {
                var description = _notificationProvider.FindDescriptionByName(name);

                var count = description.ArgumentCount;

                var arguments = new HashSet<string>();

                for (var index = 0; index < count; index++)
                {
                    arguments.Add("Argument" + index);
                }

                var metadata = this.CreateMetadata(arguments.ToArray());

                admin.Notify(name, null, metadata);
            }
        }
    }

    internal sealed partial class NotificationAggregateFactory
    {
        private readonly NotificationProvider _notificationProvider = NotificationProvider.Instance;

        public Notification CreateNotification(User user, string name, string redirectUrl, INotificationMetadata metadata)
        {
            return new Notification(user, name, redirectUrl, metadata);
        }

        public NotificationDescription CreateDescription(string name, string title, string template)
        {
            return new NotificationDescription(name, title, template);
        }

        public INotificationMetadata CreateMetadata(string[] arguments = null)
        {
            var metadata = new NotificationMetadata();

            if (arguments == null || !arguments.Any())
            {
                return metadata;
            }

            foreach (var argument in arguments)
            {
                metadata.Add(argument);
            }

            return metadata;
        }
    }
}