// Filename: UserEmailChangedIntegrationEvent.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe;
using eDoxa.ServiceBus;

namespace eDoxa.Cashier.Application.IntegrationEvents
{
    public sealed class UserEmailChangedIntegrationEvent : IntegrationEvent
    {
        public UserEmailChangedIntegrationEvent(CustomerId customerId, string email)
        {
            CustomerId = customerId;
            Email = email;
        }

        public CustomerId CustomerId { get; private set; }

        public string Email { get; private set; }
    }
}