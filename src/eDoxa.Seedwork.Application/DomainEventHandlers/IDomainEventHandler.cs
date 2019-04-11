// Filename: IDomainEventHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright � 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;

using MediatR;

namespace eDoxa.Seedwork.Application.DomainEventHandlers
{
    public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
    {
    }
}