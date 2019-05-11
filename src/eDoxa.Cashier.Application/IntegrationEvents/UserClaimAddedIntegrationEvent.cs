// Filename: UserClaimAddedIntegrationEvent.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.ServiceBus;

namespace eDoxa.Cashier.Application.IntegrationEvents
{
    public class UserClaimAddedIntegrationEvent : IntegrationEvent
    {
        public UserClaimAddedIntegrationEvent(Guid userId, string type, string value)
        {
            UserId = userId;
            Type = type;
            Value = value;
        }

        public Guid UserId { get; private set; }

        public string Type { get; private set; }

        public string Value { get; private set; }
    }
}