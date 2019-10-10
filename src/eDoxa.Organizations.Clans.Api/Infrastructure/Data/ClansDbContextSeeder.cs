// Filename: ClansDbContextSeeder.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Infrastructure.Data.Storage;
using eDoxa.Organizations.Clans.Domain.Repositories;
using eDoxa.Organizations.Clans.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Organizations.Clans.Api.Infrastructure.Data
{
    internal sealed class ClansDbContextSeeder : DbContextSeeder
    {
        private readonly ClansDbContext _context;
        private readonly IClanRepository _clanRepository;

        public ClansDbContextSeeder(
            ClansDbContext context,
            IClanRepository clanRepository,
            IHostingEnvironment environment,
            ILogger<ClansDbContextSeeder> logger
        ) : base(environment, logger)
        {
            _context = context;
            _clanRepository = clanRepository;
        }

        protected override async Task SeedDevelopmentAsync()
        {
            if (!_context.Clans.Any())
            {
                foreach (var clan in FileStorage.Clans)
                {
                    _clanRepository.Create(clan);
                }

                await _clanRepository.UnitOfWork.CommitAsync();
            }
        }
    }
}
