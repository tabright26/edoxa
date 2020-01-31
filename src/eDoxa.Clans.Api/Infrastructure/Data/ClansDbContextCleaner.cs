// Filename: ClansDbContextCleaner.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Repositories;
using eDoxa.Clans.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Clans.Api.Infrastructure.Data
{
    internal sealed class ClansDbContextCleaner : DbContextCleaner
    {
        private readonly IClanRepository _clanRepository;

        public ClansDbContextCleaner(
            IClanRepository clanRepository,
            ClansDbContext context,
            IWebHostEnvironment environment,
            ILogger<ClansDbContextCleaner> logger
        ) : base(context, environment, logger)
        {
            _clanRepository = clanRepository;
            Clans = context.Set<Clan>();
        }

        private DbSet<Clan> Clans { get; }

        protected override void Cleanup()
        {
            foreach (var clan in Clans)
            {
                _clanRepository.DeleteLogoAsync(clan.Id).GetAwaiter().GetResult();

                Clans.Remove(clan);
            }
        }
    }
}
