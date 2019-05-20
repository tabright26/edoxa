// Filename: CashierDbContext.cs
// Date Created: 2019-05-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Infrastructure.Configurations;
using eDoxa.Seedwork.Infrastructure;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Infrastructure
{
    public sealed partial class CashierDbContext
    {
        public async Task SeedAsync(ILogger logger)
        {
            if (!Users.Any())
            {
                var user = new User(
                    UserId.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"),
                    new StripeAccountId("acct_1EbASfAPhMnJQouG"),
                    new StripeCustomerId("cus_F5L8mRzm6YN5ma")
                );

                user.AddBankAccount(new StripeBankAccountId("ba_1EbB3sAPhMnJQouGHsvc0NFn"));

                Users.Add(user);

                await this.CommitAsync();

                logger.LogInformation("The user's being populated.");
            }
            else
            {
                logger.LogInformation("The user's already populated.");
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

        public DbSet<User> Users => this.Set<User>();

        public DbSet<MoneyAccount> MoneyAccounts => this.Set<MoneyAccount>();

        public DbSet<MoneyTransaction> MoneyTransactions => this.Set<MoneyTransaction>();

        public DbSet<TokenAccount> TokenAccounts => this.Set<TokenAccount>();

        public DbSet<TokenTransaction> TokenTransactions => this.Set<TokenTransaction>();

        protected override void OnModelCreating(ModelBuilder builder)
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
