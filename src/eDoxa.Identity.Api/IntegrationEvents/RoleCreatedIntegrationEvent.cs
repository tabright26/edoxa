// Filename: RoleCreatedIntegrationEvent.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
