// Filename: MediatorExtensions.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.SqlServer;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Seedwork.Infrastructure.MediatR.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task PublishDomainEventAsync<TDomainEvent>(this IMediator mediator, TDomainEvent domainEvent)
        where TDomainEvent : IDomainEvent
        {
            await mediator.Publish(domainEvent);
        }

        public static async Task PublishDomainEventsAsync(this IMediator mediator, DbContext context, bool publishDomainEvents)
        {
            var entities = context.ChangeTracker.Entries<IEntityModel>()
                .Select(entry => entry.Entity)
                .Where(entity => entity.DomainEvents?.Any() ?? false)
                .ToList();

            if (publishDomainEvents)
            {
                var domainEvents = entities.SelectMany(entity => entity.DomainEvents).ToList();

                foreach (var entity in entities)
                {
                    entity.DomainEvents.Clear();
                }

                foreach (var domainEvent in domainEvents)
                {
                    await mediator.PublishDomainEventAsync(domainEvent);
                }
            }
            else
            {
                foreach (var entity in entities)
                {
                    entity.DomainEvents.Clear();
                }
            }
        }
    }
}
