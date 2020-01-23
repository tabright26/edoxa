// Filename: CashierDbContextCleaner.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace eDoxa.Cashier.Api.Infrastructure.Data
{
    internal sealed class CashierDbContextCleaner : IDbContextCleaner
    {
        private readonly CashierDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CashierDbContextCleaner(IWebHostEnvironment environment, CashierDbContext context)
        {
            _environment = environment;
            _context = context;
        }

        private DbSet<AccountModel> Accounts => _context.Set<AccountModel>();

        private DbSet<ChallengePayoutModel> ChallengePayouts => _context.Set<ChallengePayoutModel>();

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                Accounts.RemoveRange(Accounts);

                ChallengePayouts.RemoveRange(ChallengePayouts);

                await _context.SaveChangesAsync();
            }
        }
    }
}
