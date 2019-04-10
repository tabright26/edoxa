// Filename: UserPhoneNumberChangedIntegrationEvent.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.ServiceBus;

namespace eDoxa.Cashier.Application.IntegrationEvents
{
    public class UserPhoneNumberChangedIntegrationEvent : IntegrationEvent
    {
        public UserPhoneNumberChangedIntegrationEvent(UserId userId, string phoneNumber)
        {
            UserId = userId;
            PhoneNumber = phoneNumber;
        }

        public UserId UserId { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}