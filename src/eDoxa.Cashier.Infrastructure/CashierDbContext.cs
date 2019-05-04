// Filename: CashierDbContext.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
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
            if (!MoneyAccounts.Any())
            {
                MoneyAccounts.Add(new MoneyAccount(_userAggregateFactory.CreateAdminId()));

                await this.SaveChangesAsync();

                logger.LogInformation("The money accounts being populated.");
            }
            else
            {
                logger.LogInformation("The money accounts already populated.");
            }

            if (!TokenAccounts.Any())
            {
                TokenAccounts.Add(new TokenAccount(_userAggregateFactory.CreateAdminId()));

                await this.SaveChangesAsync();

                logger.LogInformation("The token accounts being populated.");
            }
            else
            {
                logger.LogInformation("The token accounts already populated.");
            }
        }
    }

    public sealed partial class CashierDbContext : CustomDbContext
    {
        public CashierDbContext(DbContextOptions<CashierDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        internal CashierDbContext(DbContextOptions<CashierDbContext> options) : base(options)
        {
        }

        public DbSet<MoneyAccount> MoneyAccounts => this.Set<MoneyAccount>();

        public DbSet<MoneyTransaction> MoneyTransactions => this.Set<MoneyTransaction>();

        public DbSet<TokenAccount> TokenAccounts => this.Set<TokenAccount>();

        public DbSet<TokenTransaction> TokenTransactions => this.Set<TokenTransaction>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(nameof(eDoxa).ToLower());

            builder.ApplyConfiguration(new MoneyAccountConfiguration());

            builder.ApplyConfiguration(new MoneyTransactionConfiguration());

            builder.ApplyConfiguration(new TokenAccountConfiguration());

            builder.ApplyConfiguration(new TokenTransactionConfiguration());
        }
    }
}