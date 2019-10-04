// Filename: DomainEventHandler.cs
// Date Created: 2019-09-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using MediatR;

namespace eDoxa.Seedwork.Domain
{
    public abstract class DomainEventHandler<TDomainEvent> : NotificationHandler<TDomainEvent>, IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
    {
    }
}
