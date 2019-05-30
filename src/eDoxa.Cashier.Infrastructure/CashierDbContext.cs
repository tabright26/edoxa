// Filename: CashierDbContext.cs
// Date Created: 2019-05-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Infrastructure.Configurations;
using eDoxa.Seedwork.Infrastructure;

using JetBrains.Annotations;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure
{
    public sealed class CashierDbContext : CustomDbContext
    {
        public CashierDbContext(DbContextOptions<CashierDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        public DbSet<User> Users => this.Set<User>();

        public DbSet<Account> Accounts => this.Set<Account>();

        public DbSet<Transaction> Transactions => this.Set<Transaction>();

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(nameof(eDoxa).ToLower());

            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new AccountConfiguration());

            builder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}
