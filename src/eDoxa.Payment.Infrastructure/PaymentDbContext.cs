// Filename: PaymentDbContext.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Models;
using eDoxa.Seedwork.Domain;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Payment.Infrastructure
{
    public sealed class PaymentDbContext : DbContext, IUnitOfWork
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }

        public DbSet<StripeReference> StripeReferences => this.Set<StripeReference>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StripeReference>(
                builder =>
                {
                    builder.HasKey(reference => reference.UserId);

                    builder.Property(reference => reference.UserId).HasConversion(userId => userId.ToGuid(), userId => UserId.FromGuid(userId)).IsRequired();

                    builder.Property(reference => reference.CustomerId).IsRequired();

                    builder.Property(reference => reference.AccountId).IsRequired();

                    builder.ToTable("StripeReference");
                });
        }

        public async Task CommitAsync(bool dispatchDomainEvents = true, CancellationToken cancellationToken = default)
        {
            await this.SaveChangesAsync(cancellationToken);
        }
    }
}
