// Filename: UserProfileCreatedDomainEvent.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate.DomainEvents
{
    public sealed class UserProfileCreatedDomainEvent : IDomainEvent
    {
        public UserProfileCreatedDomainEvent(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; private set; }
    }
}