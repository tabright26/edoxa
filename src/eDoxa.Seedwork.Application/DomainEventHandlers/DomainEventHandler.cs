// Filename: DomainEventHandler.cs
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
    public abstract class DomainEventHandler<TDomainEvent> : NotificationHandler<TDomainEvent>, IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
    {
    }
}