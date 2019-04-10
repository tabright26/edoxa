// Filename: RoleClaimAddedIntegrationEvent.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.ServiceBus;

namespace eDoxa.Identity.Application.IntegrationEvents
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