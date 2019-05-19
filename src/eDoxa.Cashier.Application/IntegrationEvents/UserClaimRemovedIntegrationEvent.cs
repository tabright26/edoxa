﻿// Filename: UserClaimRemovedIntegrationEvent.cs
// Date Created: 2019-05-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.ServiceBus;

namespace eDoxa.Cashier.Application.IntegrationEvents
{
    public sealed class UserClaimRemovedIntegrationEvent : IntegrationEvent
    {
        public UserClaimRemovedIntegrationEvent(Guid userId, IDictionary<string, string> claims)
        {
            UserId = userId;
            Claims = claims;
        }

        public UserClaimRemovedIntegrationEvent(Guid userId, string type, string value)
        {
            UserId = userId;

            Claims = new Dictionary<string, string>
            {
                [type] = value
            };
        }

        public Guid UserId { get; private set; }

        public IDictionary<string, string> Claims { get; private set; }
    }
}
