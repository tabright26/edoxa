// Filename: UserClaimsRemovedIntegrationEvent.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.ServiceBus;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    public sealed class UserClaimsRemovedIntegrationEvent : IntegrationEvent
    {
        public UserClaimsRemovedIntegrationEvent() : base(Guid.NewGuid())
        {
            
        }

        public Guid UserId { get; set; }

        public IDictionary<string, string> Claims { get; set; }
    }
}
