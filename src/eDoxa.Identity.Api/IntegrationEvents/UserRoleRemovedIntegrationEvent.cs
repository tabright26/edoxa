﻿// Filename: UserRoleRemovedIntegrationEvent.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.IntegrationEvents;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    public class UserRoleRemovedIntegrationEvent : IntegrationEvent
    {
        public UserRoleRemovedIntegrationEvent(Guid userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }

        public Guid UserId { get; private set; }

        public string RoleName { get; private set; }
    }
}