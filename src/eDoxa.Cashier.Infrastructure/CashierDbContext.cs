﻿// Filename: CashierDbContext.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Infrastructure.Configurations;
using eDoxa.Seedwork.Infrastructure;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Infrastructure
{
    public sealed partial class CashierDbContext
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        public async Task SeedAsync(ILogger logger)
        {
            if (!Users.Any())
            {
                Users.AddRange(
                    _userAggregateFactory.CreateAdmin(),
                    _userAggregateFactory.CreateFrancis(),
                    _userAggregateFactory.CreateRoy(),
                    _userAggregateFactory.CreateRyan()
                );

                await this.SaveChangesAsync();

                logger.LogInformation("The users being populated.");
            }
            else
            {
                logger.LogInformation("The users already populated.");
            }
        }
    }

    public sealed partial class CashierDbContext : CustomDbContext
    {
        public CashierDbContext(DbContextOptions<CashierDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        public DbSet<User> Users
        {
            get
            {
                return this.Set<User>();
            }
        }

        public DbSet<Account> Accounts
        {
            get
            {
                return this.Set<Account>();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(nameof(eDoxa).ToLower());

            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new AccountConfiguration());
        }
    }
}