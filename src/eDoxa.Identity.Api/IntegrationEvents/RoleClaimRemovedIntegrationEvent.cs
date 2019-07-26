﻿// Filename: RoleClaimRemovedIntegrationEvent.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.IntegrationEvents;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    public class RoleClaimRemovedIntegrationEvent : IntegrationEvent
    {
        public RoleClaimRemovedIntegrationEvent(string roleName, string claimType, string claimValue)
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
