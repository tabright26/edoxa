// Filename: DomainEventHandlerExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Seedwork.Application.Extensions
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
