// Filename: ChallengesDbContext.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Infrastructure.Configurations;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Extensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

namespace eDoxa.Challenges.Infrastructure
{
    public sealed class ChallengesDbContext : DbContext, IUnitOfWork
    {
        public ChallengesDbContext(DbContextOptions<ChallengesDbContext> options, IMediator mediator) : this(options)
        {
            Mediator = mediator;
        }

        public ChallengesDbContext(DbContextOptions<ChallengesDbContext> options) : base(options)
        {
            Mediator = new Mock<IMediator>().Object;
        }

        private IMediator Mediator { get; }

        public DbSet<ChallengeModel> Challenges => this.Set<ChallengeModel>();

        public DbSet<ParticipantModel> Participants => this.Set<ParticipantModel>();

        public DbSet<MatchModel> Matches => this.Set<MatchModel>();

        public async Task CommitAsync(bool dispatchDomainEvents = true, CancellationToken cancellationToken = default)
        {
            await this.SaveChangesAsync(cancellationToken);

            if (dispatchDomainEvents)
            {
                var entities = ChangeTracker.Entries<IEntityModel>().Select(entry => entry.Entity).Where(entity => entity.DomainEvents.Any()).ToList();

                var domainEvents = entities.SelectMany(entity => entity.DomainEvents).ToList();

                foreach (var entity in entities)
                {
                    entity.DomainEvents.Clear();
                }

                foreach (var domainEvent in domainEvents)
                {
                    await Mediator.PublishDomainEventAsync(domainEvent);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ChallengeModelConfiguration());

            builder.ApplyConfiguration(new ParticipantModelConfiguration());

            builder.ApplyConfiguration(new MatchModelConfiguration());
        }
    }
}
