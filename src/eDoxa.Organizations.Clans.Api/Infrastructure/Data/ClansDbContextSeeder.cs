// Filename: ClansDbContextSeeder.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Organizations.Clans.Api.Infrastructure.Data
{
    internal sealed class ClansDbContextSeeder : IDbContextSeeder
    {
        private readonly ClansDbContext _context;
        private readonly ILogger<ClansDbContextSeeder> _logger;
        private readonly IHostingEnvironment _environment;

        public ClansDbContextSeeder(ILogger<ClansDbContextSeeder> logger, IHostingEnvironment environment, ClansDbContext context)
        {
            _logger = logger;
            _environment = environment;
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Clans.Any())
                {
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("The clan's being populated.");
                }
                else
                {
                    _logger.LogInformation("The clan's already populated.");
                }
            }
        }
    }
}
