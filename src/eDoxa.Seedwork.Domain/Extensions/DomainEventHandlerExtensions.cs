// Filename: DomainEventHandlerExtensions.cs
// Date Created: 2019-08-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Seedwork.Domain.Extensions
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
