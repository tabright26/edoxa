// Filename: ClansDbContextSeeder.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Clans.Api.Infrastructure.Data.Storage;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Clans.Api.Infrastructure.Data
{
    internal sealed class ClansDbContextSeeder : DbContextSeeder
    {
        public ClansDbContextSeeder(ClansDbContext context, IWebHostEnvironment environment, ILogger<ClansDbContextSeeder> logger) : base(
            context,
            environment,
            logger)
        {
            Clans = context.Set<Clan>();
        }

        private DbSet<Clan> Clans { get; }

        protected override async Task SeedDevelopmentAsync()
        {
            if (!await Clans.AnyAsync())
            {
                Clans.AddRange(FileStorage.Clans);

                await this.CommitAsync();
            }
        }
    }
}
