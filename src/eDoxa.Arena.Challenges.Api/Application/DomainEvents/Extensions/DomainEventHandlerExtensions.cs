// Filename: DomainEventHandlerExtensions.cs
// Date Created: 2019-06-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.DomainEvents.Abstractions;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Api.Application.DomainEvents.Extensions
{
    public static class DomainEventHandlerExtensions
    {
        public static async Task HandleAsync<TDomainEvent>(this IDomainEventHandler<TDomainEvent> handler, TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent
        {
            await handler.Handle(domainEvent, default);
        }
    }
}
