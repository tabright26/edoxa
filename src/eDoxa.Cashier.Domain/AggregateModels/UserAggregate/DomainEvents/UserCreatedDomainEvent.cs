// Filename: UserCreatedDomainEvent.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate.DomainEvents
{
    public class UserCreatedDomainEvent : IDomainEvent
    {
        public UserCreatedDomainEvent(UserId userId, CustomerId customerId)
        {
            UserId = userId;
            CustomerId = customerId;
        }

        public UserId UserId { get; private set; }

        public CustomerId CustomerId { get; private set; }
    }
}