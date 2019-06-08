// Filename: UserClaimReplacedIntegrationEvent.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.IntegrationEvents;

namespace eDoxa.IdentityServer.IntegrationEvents
{
    public sealed class UserClaimReplacedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }

        public int ClaimCount { get; set; }

        public IDictionary<string, string> Claims { get; set; }

        public IDictionary<string, string> NewClaims { get; set; }
    }
}
