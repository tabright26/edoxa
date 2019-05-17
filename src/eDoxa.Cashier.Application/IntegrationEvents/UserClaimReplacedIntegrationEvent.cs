// Filename: UserClaimReplacedIntegrationEvent.cs
// Date Created: 2019-05-17
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
    public sealed class UserClaimReplacedIntegrationEvent : IntegrationEvent
    {
        public UserClaimReplacedIntegrationEvent(Guid userId, int claimCount, IDictionary<string, string> claims, IDictionary<string, string> newClaims)
        {
            UserId = userId;
            ClaimCount = claimCount;
            Claims = claims;
            NewClaims = newClaims;
        }

        public Guid UserId { get; private set; }

        public int ClaimCount { get; private set; }

        public IDictionary<string, string> Claims { get; private set; }

        public IDictionary<string, string> NewClaims { get; private set; }
    }
}