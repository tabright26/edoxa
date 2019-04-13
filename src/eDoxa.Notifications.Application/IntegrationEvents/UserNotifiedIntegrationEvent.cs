// Filename: UserNotifiedIntegrationEvent.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.ServiceBus;

namespace eDoxa.Notifications.Application.IntegrationEvents
{
    public class UserNotifiedIntegrationEvent : IntegrationEvent
    {
        public UserNotifiedIntegrationEvent(UserId userId, string title, string message, string redirectUrl = null)
        {
            UserId = userId;
            Title = title;
            Message = message;
            RedirectUrl = redirectUrl;
        }

        public UserId UserId { get; private set; }

        public string Title { get; private set; }

        public string Message { get; private set; }

        public string RedirectUrl { get; private set; }
    }
}