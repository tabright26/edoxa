// Filename: CashierDbContext.cs
// Date Created: 2019-05-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
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

        public DbSet<MoneyAccount> MoneyAccounts => this.Set<MoneyAccount>();

        public DbSet<MoneyTransaction> MoneyTransactions => this.Set<MoneyTransaction>();

        public DbSet<TokenAccount> TokenAccounts => this.Set<TokenAccount>();

        public DbSet<TokenTransaction> TokenTransactions => this.Set<TokenTransaction>();

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(nameof(eDoxa).ToLower());

            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new MoneyAccountConfiguration());

            builder.ApplyConfiguration(new MoneyTransactionConfiguration());

            builder.ApplyConfiguration(new TokenAccountConfiguration());

            builder.ApplyConfiguration(new TokenTransactionConfiguration());
        }
    }
}
