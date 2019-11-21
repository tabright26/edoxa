// Filename: CashierDbContextCleaner.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
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

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                _context.Accounts.RemoveRange(_context.Accounts);

                _context.Challenges.RemoveRange(_context.Challenges);

                await _context.SaveChangesAsync();
            }
        }
    }
}
