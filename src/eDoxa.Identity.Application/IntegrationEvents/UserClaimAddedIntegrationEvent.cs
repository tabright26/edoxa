// Filename: UserClaimAddedIntegrationEvent.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.ServiceBus;

using Newtonsoft.Json;

namespace eDoxa.Identity.Application.IntegrationEvents
{
    public sealed class UserClaimAddedIntegrationEvent : IntegrationEvent
    {
        [JsonConstructor]
        public UserClaimAddedIntegrationEvent(Guid userId, IDictionary<string, string> claims)
        {
            UserId = userId;
            Claims = claims;
        }

        public UserClaimAddedIntegrationEvent(Guid userId, string type, string value)
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