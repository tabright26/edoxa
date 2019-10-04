// Filename: ClansDbContextSeeder.cs
// Date Created: 2019-09-29
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
    internal sealed class ClansDbContextSeeder : DbContextSeeder
    {
        private readonly ClansDbContext _context;

        public ClansDbContextSeeder(ClansDbContext context, IHostingEnvironment environment, ILogger<ClansDbContextSeeder> logger) : base(environment, logger)
        {
            _context = context;
        }

        protected override async Task SeedDevelopmentAsync()
        {
            if (!_context.Clans.Any())
            {
                await _context.SaveChangesAsync();

                Logger.LogInformation("The clan's being populated.");
            }
            else
            {
                Logger.LogInformation("The clan's already populated.");
            }
        }
    }
}
