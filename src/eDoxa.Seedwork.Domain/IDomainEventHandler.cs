// Filename: IDomainEventHandler.cs
// Date Created: 2019-09-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using MediatR;

namespace eDoxa.Seedwork.Domain
{
    public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
    {
    }
}
