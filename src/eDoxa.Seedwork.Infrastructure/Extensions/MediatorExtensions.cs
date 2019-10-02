// Filename: MediatorExtensions.cs
// Date Created: 2019-10-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Mediator.Abstractions;

using MediatR;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class MediatorExtensions
    {
        public static Task PublishDomainEventAsync<TDomainEvent>(this IMediator mediator, TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent
        {
            return mediator.Publish(domainEvent);
        }
    }
}
