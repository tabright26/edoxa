// Filename: RoleDeletedIntegrationEvent.cs
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
    public class RoleDeletedIntegrationEvent : IntegrationEvent
    {
        public RoleDeletedIntegrationEvent(string roleName)
        {
            RoleName = roleName;
        }

        public string RoleName { get; private set; }
    }
}