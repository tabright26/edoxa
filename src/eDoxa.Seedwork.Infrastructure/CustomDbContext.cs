// Filename: CustomDbContext.cs
// Date Created: 2019-04-21
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
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Infrastructure.Extensions;

using JetBrains.Annotations;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

namespace eDoxa.Seedwork.Infrastructure
{
    public abstract class CustomDbContext : DbContext, IUnitOfWork
    {
        private const string DefaultSchema = "dbo";

        private readonly IMediator _mediator;

        protected CustomDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected CustomDbContext(DbContextOptions options) : base(options)
        {
            var mock = new Mock<IMediator>();

            _mediator = mock.Object;
        }

        public DbSet<LogEntry> Logs => this.Set<LogEntry>();

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

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            builder.Entity<LogEntry>(
                entity =>
                {
                    entity.ToTable(nameof(Logs), DefaultSchema);

                    entity.Property(logEntry => logEntry.Id).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.Date).IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.Version).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.Method).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.Url).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.LocalIpAddress).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.RemoteIpAddress).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.Origin).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.RequestBody).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.RequestType).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.ResponseBody).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.ResponseType).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.Property(logEntry => logEntry.IdempotencyKey).IsRequired(false).UsePropertyAccessMode(PropertyAccessMode.Field);

                    entity.HasKey(logEntry => logEntry.Id);

                    entity.HasIndex(logEntry => logEntry.IdempotencyKey).IsUnique();
                }
            );
        }
    }
}