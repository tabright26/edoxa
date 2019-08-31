// Filename: DomainEventHandler.cs
// Date Created: 2019-08-30
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
