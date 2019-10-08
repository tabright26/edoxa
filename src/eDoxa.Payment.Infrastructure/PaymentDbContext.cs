﻿// Filename: PaymentDbContext.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Payment.Infrastructure
{
    public sealed class PaymentDbContext : DbContext
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

                    builder.Property(reference => reference.ConnectAccountId).IsRequired();

                    builder.ToTable("StripeReference");
                });
        }
    }
}