// Filename: UserNotifiedIntegrationEvent.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.ServiceBus;

namespace eDoxa.Notifications.Application.IntegrationEvents
{
    public class UserNotifiedIntegrationEvent : IntegrationEvent
    {
        public UserNotifiedIntegrationEvent(UserId userId, string name, string redirectUrl = null, NotificationMetadata metadata = null)
        {
            UserId = userId;
            Name = name;
            RedirectUrl = redirectUrl;
            Metadata = metadata;
        }

        public UserId UserId { get; private set; }

        public string Name { get; private set; }

        public string RedirectUrl { get; private set; }

        public NotificationMetadata Metadata { get; private set; }
    }
}