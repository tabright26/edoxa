﻿// Filename: RoleCreatedIntegrationEvent.cs
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
    public class RoleCreatedIntegrationEvent : IntegrationEvent
    {
        public RoleCreatedIntegrationEvent(string roleName)
        {
            RoleName = roleName;
        }

        public string RoleName { get; private set; }
    }
}