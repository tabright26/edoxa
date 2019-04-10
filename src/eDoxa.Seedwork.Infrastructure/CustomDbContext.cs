// Filename: CustomDbContext.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Infrastructure.Repositories;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Seedwork.Infrastructure
{
    public abstract class CustomDbContext : DbContext, IUnitOfWork
    {
        private const string DefaultSchema = "dbo";

        private readonly IMediator _mediator;

        protected CustomDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.SaveChangesAsync(cancellationToken);
        }

        public async Task CommitAndDispatchDomainEventsAsync(CancellationToken cancellationToken = default(CancellationToken))
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

        public DbSet<RequestLogEntry> RequestLogs
        {
            get
            {
                return this.Set<RequestLogEntry>();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RequestLogEntry>(
                entity =>
                {
                    entity.ToTable(nameof(RequestLogs), DefaultSchema);
                    entity.HasKey(logEntry => logEntry.Id);
                    entity.Property(logEntry => logEntry.Id).IsRequired();
                    entity.Property(logEntry => logEntry.Time).IsRequired();
                    entity.Property(logEntry => logEntry.Type).IsRequired();
                    entity.Property(logEntry => logEntry.Version).IsRequired(false);
                    entity.Property(logEntry => logEntry.Method).IsRequired(false);
                    entity.Property(logEntry => logEntry.Url).IsRequired(false);
                    entity.Property(logEntry => logEntry.LocalIpAddress).IsRequired(false);
                    entity.Property(logEntry => logEntry.RemoteIpAddress).IsRequired(false);
                    entity.Property(logEntry => logEntry.Origin).IsRequired(false);
                    entity.Property(logEntry => logEntry.IdempotencyKey).IsRequired(false);

                    //TODO: This must be implemented before eDoxa v.3 (Release 1)
                    //entity.Property(logEntry => logEntry.RequestBody).IsRequired(false);
                    //entity.Property(logEntry => logEntry.RequestType).IsRequired(false);
                    //entity.Property(logEntry => logEntry.ResponseBody).IsRequired(false);
                    //entity.Property(logEntry => logEntry.ResponseType).IsRequired(false);
                    entity.HasIndex(logEntry => logEntry.IdempotencyKey).IsUnique();
                }
            );
        }
    }
}