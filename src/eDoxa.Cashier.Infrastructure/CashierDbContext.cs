// Filename: CashierDbContext.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Infrastructure.Configurations;
using eDoxa.Seedwork.Infrastructure;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Infrastructure
{
    public sealed partial class CashierDbContext
    {
        public async Task SeedAsync(ILogger logger, IConfiguration configuration)
        {
            if (!MoneyAccounts.Any() || !TokenAccounts.Any())
            {
                configuration.GetSection("Users").Get<List<string>>().ForEach(userId =>
                {
                    MoneyAccounts.Add(new MoneyAccount(UserId.Parse(userId)));

                    TokenAccounts.Add(new TokenAccount(UserId.Parse(userId)));
                });

                await this.CommitAsync();

                logger.LogInformation("The accounts being populated.");
            }
            else
            {
                logger.LogInformation("The accounts already populated.");
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