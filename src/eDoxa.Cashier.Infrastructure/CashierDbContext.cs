// Filename: CashierDbContext.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Infrastructure.Configurations;
using eDoxa.Cashier.Infrastructure.Models;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure
{
    public sealed class CashierDbContext : DbContext
    {
        public CashierDbContext(DbContextOptions<CashierDbContext> options) : base(options)
        {
        }

        public DbSet<AccountModel> Accounts => this.Set<AccountModel>();

        public DbSet<TransactionModel> Transactions => this.Set<TransactionModel>();

        public DbSet<ChallengeModel> Challenges => this.Set<ChallengeModel>();

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AccountModelConfiguration());

            builder.ApplyConfiguration(new TransactionModelConfiguration());

            builder.ApplyConfiguration(new ChallengeModelConfiguration());
        }
    }
}
