// Filename: DomainEventHandler.cs
// Date Created: 2019-06-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;

using MediatR;

namespace eDoxa.Arena.Challenges.Api.Application.DomainEvents.Abstractions
{
    public abstract class DomainEventHandler<TDomainEvent> : NotificationHandler<TDomainEvent>, IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
    {
    }
}
