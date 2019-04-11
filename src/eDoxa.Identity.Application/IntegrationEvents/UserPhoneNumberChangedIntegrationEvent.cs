﻿// Filename: UserPhoneNumberChangedIntegrationEvent.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.ServiceBus;

namespace eDoxa.Identity.Application.IntegrationEvents
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