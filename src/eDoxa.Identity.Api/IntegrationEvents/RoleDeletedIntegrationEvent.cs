// Filename: RoleDeletedIntegrationEvent.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.ServiceBus;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    public class RoleDeletedIntegrationEvent : IntegrationEvent
    {
        public RoleDeletedIntegrationEvent(string roleName) : base(Guid.NewGuid())
        {
            RoleName = roleName;
        }

        public string RoleName { get; private set; }
    }
}
