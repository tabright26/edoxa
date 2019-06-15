// Filename: CashierDbContextData.cs
// Date Created: 2019-06-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Fakers;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.Infrastructure.Data
{
    internal sealed class CashierDbContextData : IDbContextData
    {
        private readonly CashierDbContext _context;
        private readonly ILogger<CashierDbContextData> _logger;
        private readonly IHostingEnvironment _environment;

        public CashierDbContextData(ILogger<CashierDbContextData> logger, IHostingEnvironment environment, CashierDbContext context)
        {
            _logger = logger;
            _environment = environment;
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Users.Any())
                {
                    var userFaker = new UserFaker();

                    var user = userFaker.FakeAdminUser();

                    _context.Users.Add(user);

                    await _context.CommitAsync();

                    _logger.LogInformation("The user's being populated.");
                }
                else
                {
                    _logger.LogInformation("The user's already populated.");
                }
            }
        }

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                _context.Users.RemoveRange(_context.Users);

                await _context.SaveChangesAsync();
            }
        }
    }
}
