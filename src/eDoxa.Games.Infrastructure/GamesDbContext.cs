// Filename: GamesDbContext.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Infrastructure.Extensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

namespace eDoxa.Games.Infrastructure
{
    public sealed class GamesDbContext : DbContext, IUnitOfWork
    {
        public GamesDbContext(DbContextOptions<GamesDbContext> options, IMediator mediator) : this(options)
        {
            Mediator = mediator;
        }

        public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
        {
            Mediator = new Mock<IMediator>().Object;
        }

        private IMediator Mediator { get; }

        public DbSet<Credential> Credentials => this.Set<Credential>();

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
            modelBuilder.Entity<Credential>(
                builder =>
                {
                    builder.Property(credential => credential.UserId).HasConversion<Guid>(userId => userId, value => UserId.FromGuid(value)).IsRequired();

                    builder.Property(credential => credential.Game).HasConversion(game => game.Value, value => Game.FromValue(value)).IsRequired();

                    builder.Property(credential => credential.PlayerId).HasConversion<string>(userId => userId, value => PlayerId.Parse(value)).IsRequired();

                    builder.Property(credential => credential.Timestamp).HasConversion(dateTime => dateTime.Ticks, value => new DateTime(value)).IsRequired();

                    builder.HasKey(
                        credential => new
                        {
                            credential.UserId,
                            credential.Game
                        });

                    builder.HasIndex(
                            credential => new
                            {
                                credential.Game,
                                credential.PlayerId
                            })
                        .IsUnique();

                    builder.ToTable("Credential");
                });
        }
    }
}
