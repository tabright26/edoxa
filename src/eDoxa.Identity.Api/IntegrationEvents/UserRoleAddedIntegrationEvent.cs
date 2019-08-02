// Filename: UserRoleAddedIntegrationEvent.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.ServiceBus;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    public class UserRoleAddedIntegrationEvent : IntegrationEvent
    {
        public UserRoleAddedIntegrationEvent(Guid userId, string roleName) : base(Guid.NewGuid())
        {
            UserId = userId;
            RoleName = roleName;
        }

        public Guid UserId { get; private set; }

        public string RoleName { get; private set; }
    }
}
