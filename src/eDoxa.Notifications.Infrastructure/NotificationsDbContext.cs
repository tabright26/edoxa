// Filename: NotificationsDbContext.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.Infrastructure.Configurations;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.MediatR.Extensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

namespace eDoxa.Notifications.Infrastructure
{
    public sealed class NotificationsDbContext : DbContext, IUnitOfWork
    {
        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options, IMediator mediator) : this(options)
        {
            Mediator = mediator;
        }

        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) : base(options)
        {
            Mediator = new Mock<IMediator>().Object;
        }

        private IMediator Mediator { get; }

        public DbSet<User> Users => this.Set<User>();

        public async Task CommitAsync(bool dispatchDomainEvents = true, CancellationToken cancellationToken = default)
        {
            await this.SaveChangesAsync(cancellationToken);

            if (dispatchDomainEvents)
            {
                var entities = ChangeTracker.Entries<IEntity>().Select(entry => entry.Entity).Where(entity => entity.DomainEvents.Any()).ToList();

                var domainEvents = entities.SelectMany(entity => entity.DomainEvents).ToList();

                foreach (var entity in entities)
                {
                    entity.ClearDomainEvents();
                }

                foreach (var domainEvent in domainEvents)
                {
                    await Mediator.PublishDomainEventAsync(domainEvent);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserModelConfiguration());
        }
    }
}
