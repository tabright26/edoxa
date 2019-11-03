// Filename: ClansDbContextCleaner.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Clans.Domain.Repositories;
using eDoxa.Clans.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Clans.Api.Infrastructure.Data
{
    internal sealed class ClansDbContextCleaner : IDbContextCleaner
    {
        private readonly ClansDbContext _context;
        private readonly IClanRepository _clanRepository;
        private readonly IHostingEnvironment _environment;

        public ClansDbContextCleaner(IClanRepository clanRepository, IHostingEnvironment environment, ClansDbContext context)
        {
            _clanRepository = clanRepository;
            _environment = environment;
            _context = context;
        }

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                var clans = _context.Clans;

                foreach (var clan in clans)
                {
                    await _clanRepository.DeleteLogoAsync(clan.Id);
                }

                _context.Clans.RemoveRange(clans);

                await _context.SaveChangesAsync();
            }
        }
    }
}
