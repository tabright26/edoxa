// Filename: CashierDbContext.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Infrastructure.Configurations;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.MediatR.Extensions;

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

        public async Task CommitAsync(bool publishDomainEvents = true, CancellationToken cancellationToken = default)
        {
            await this.SaveChangesAsync(cancellationToken);

            await Mediator.PublishDomainEventsAsync(this, publishDomainEvents);
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
