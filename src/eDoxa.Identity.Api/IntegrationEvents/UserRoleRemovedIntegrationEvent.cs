// Filename: UserRoleRemovedIntegrationEvent.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.IntegrationEvents;

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
