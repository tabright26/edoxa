// Filename: CashierDbContext.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Infrastructure.Configurations;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.MediatR.Extensions;
using eDoxa.Seedwork.Infrastructure.SqlServer;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Moq;

namespace eDoxa.Cashier.Infrastructure
{
    public sealed class CashierDbContext : DbContext, IUnitOfWork
    {
        public CashierDbContext(DbContextOptions<CashierDbContext> options, IMediator mediator) : this(options)
        {
            Mediator = mediator;
        }

        public CashierDbContext(DbContextOptions<CashierDbContext> options) : base(options)
        {
            Mediator = new Mock<IMediator>().Object;
        }

        private IMediator Mediator { get; }

        public DbSet<AccountModel> Accounts => this.Set<AccountModel>();

        public DbSet<TransactionModel> Transactions => this.Set<TransactionModel>();

        public DbSet<ChallengeModel> Challenges => this.Set<ChallengeModel>();

        public async Task CommitAsync(bool publishDomainEvents = true, CancellationToken cancellationToken = default)
        {
            await this.SaveChangesAsync(cancellationToken);

            var entities = ChangeTracker.Entries<IEntityModel>().Select(entry => entry.Entity).Where(entity => entity.DomainEvents.Any()).ToList();

            if (publishDomainEvents)
            {
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
            else
            {
                foreach (var entity in entities)
                {
                    entity.DomainEvents.Clear();
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PromotionModelConfiguration());
            builder.ApplyConfiguration(new AccountModelConfiguration());
            builder.ApplyConfiguration(new TransactionModelConfiguration());
            builder.ApplyConfiguration(new ChallengeModelConfiguration());
        }
    }
}
