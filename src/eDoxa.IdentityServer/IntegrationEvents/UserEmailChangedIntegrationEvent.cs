// Filename: UserEmailChangedIntegrationEvent.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.IntegrationEvents;

namespace eDoxa.IdentityServer.IntegrationEvents
{
    public class UserEmailChangedIntegrationEvent : IntegrationEvent
    {
        public UserEmailChangedIntegrationEvent(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        public Guid UserId { get; private set; }

        public string Email { get; private set; }
    }
}
