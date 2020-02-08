// Filename: CashierDbContextCleaner.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.Infrastructure.Data
{
    internal sealed class CashierDbContextCleaner : DbContextCleaner
    {
        public CashierDbContextCleaner(CashierDbContext context, IWebHostEnvironment environment, ILogger<CashierDbContextCleaner> logger) : base(
            context,
            environment,
            logger)
        {
            Accounts = context.Set<AccountModel>();
            Challenges = context.Set<ChallengeModel>();
            Promotions = context.Set<PromotionModel>();
        }

        private DbSet<AccountModel> Accounts { get; }

        private DbSet<ChallengeModel> Challenges { get; }

        private DbSet<PromotionModel> Promotions { get; }

        protected override void Cleanup()
        {
            Accounts.RemoveRange(Accounts);

            Challenges.RemoveRange(Challenges);

            Promotions.RemoveRange(Promotions);
        }
    }
}
