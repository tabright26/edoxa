// Filename: UserCreatedIntegrationEvent.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.ServiceBus;

namespace eDoxa.Challenges.Application.IntegrationEvents
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public UserCreatedIntegrationEvent(UserId userId)
        {
            UserId = userId;
        }

        public UserId UserId { get; private set; }
    }
}