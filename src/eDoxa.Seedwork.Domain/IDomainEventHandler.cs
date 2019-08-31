// Filename: IDomainEventHandler.cs
// Date Created: 2019-08-30
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
