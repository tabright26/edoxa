// Filename: CashierDbContext.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.IO;
using System.Reflection;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Infrastructure.Configurations;
using eDoxa.Seedwork.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Factories;

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

        private sealed class CashierDbContextFactory : DesignTimeDbContextFactory<CashierDbContext>
        {
            protected override string BasePath => Path.Combine(Directory.GetCurrentDirectory(), "../eDoxa.Cashier.Api");

            protected override Assembly MigrationsAssembly => Assembly.GetAssembly(typeof(CashierDbContextFactory));

            [NotNull]
            public override CashierDbContext CreateDbContext(string[] args)
            {
                return new CashierDbContext(Options, new NoMediator());
            }
        }
    }
}
