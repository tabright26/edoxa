// Filename: RoleClaimAddedIntegrationEvent.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.IntegrationEvents;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    public class RoleClaimAddedIntegrationEvent : IntegrationEvent
    {
        public RoleClaimAddedIntegrationEvent(string roleName, string claimType, string claimValue)
        {
            RoleName = roleName;
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        public string RoleName { get; private set; }

        public string ClaimType { get; private set; }

        public string ClaimValue { get; private set; }
    }
}
