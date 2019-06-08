// Filename: RoleDeletedIntegrationEvent.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.ServiceBus;

namespace eDoxa.Identity.Api.IntegrationEvents
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
