﻿// Filename: CashierDbContext.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Infrastructure.Configurations;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Infrastructure;

using JetBrains.Annotations;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure
{
    public sealed class CashierDbContext : DbContext
    {
        public CashierDbContext(DbContextOptions<CashierDbContext> options, IMediator mediator) : base(options)
        {
            Mediator = mediator;
        }

        public CashierDbContext(DbContextOptions<CashierDbContext> options) : base(options)
        {
            Mediator = new NoMediator();
        }

        public IMediator Mediator { get; }

        public DbSet<UserModel> Users => this.Set<UserModel>();

        public DbSet<AccountModel> Accounts => this.Set<AccountModel>();

        public DbSet<TransactionModel> Transactions => this.Set<TransactionModel>();

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserModelConfiguration());

            builder.ApplyConfiguration(new AccountModelConfiguration());

            builder.ApplyConfiguration(new TransactionModelConfiguration());
        }
    }
}
