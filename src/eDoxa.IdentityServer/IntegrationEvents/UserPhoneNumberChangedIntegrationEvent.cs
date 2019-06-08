// Filename: UserPhoneNumberChangedIntegrationEvent.cs
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
    public class UserPhoneNumberChangedIntegrationEvent : IntegrationEvent
    {
        public UserPhoneNumberChangedIntegrationEvent(Guid userId, string phoneNumber)
        {
            UserId = userId;
            PhoneNumber = phoneNumber;
        }

        public Guid UserId { get; private set; }

        public string PhoneNumber { get; private set; }
    }
}
