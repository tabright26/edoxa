// Filename: CustomDbContext.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.Extensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Seedwork.Infrastructure
{
    public abstract class CustomDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        protected CustomDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await this.SaveChangesAsync(cancellationToken);
        }

        public async Task CommitAndDispatchDomainEventsAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<IEntity>().Select(entry => entry.Entity).Where(entity => entity.DomainEvents.Any()).ToList();

            await this.SaveChangesAsync(cancellationToken);

            foreach (var entity in entities)
            {
                foreach (var domainEvent in entity.DomainEvents)
                {
                    await _mediator.PublishDomainEventsAsync(domainEvent);
                }

                entity.ClearDomainEvents();
            }
        }
    }
}
