// Filename: UserClaimsReplacedIntegrationEvent.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.IntegrationEvents;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    public sealed class UserClaimsReplacedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }

        public int ClaimCount { get; set; }

        public IDictionary<string, string> Claims { get; set; }

        public IDictionary<string, string> NewClaims { get; set; }
    }
}
