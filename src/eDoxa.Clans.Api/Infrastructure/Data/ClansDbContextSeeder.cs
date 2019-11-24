// Filename: ClansDbContextSeeder.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Infrastructure.Data.Storage;
using eDoxa.Clans.Domain.Repositories;
using eDoxa.Clans.Infrastructure;
using eDoxa.Seedwork.Application;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Clans.Api.Infrastructure.Data
{
    internal sealed class ClansDbContextSeeder : DbContextSeeder
    {
        private readonly ClansDbContext _context;
        private readonly IClanRepository _clanRepository;

        public ClansDbContextSeeder(
            ClansDbContext context,
            IClanRepository clanRepository,
            IWebHostEnvironment environment,
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
                    clan.ClearDomainEvents();

                    _clanRepository.Create(clan);
                }

                await _clanRepository.UnitOfWork.CommitAsync();
            }
        }
    }
}
