// Filename: RoleDeletedIntegrationEvent.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.IntegrationEvents;

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
