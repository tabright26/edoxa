// Filename: GamesDbContextSeeder.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Games.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Games.Api.Infrastructure.Data
{
    internal sealed class GamesDbContextSeeder : DbContextSeeder
    {
        public GamesDbContextSeeder(GamesDbContext context, IWebHostEnvironment environment, ILogger<GamesDbContextSeeder> logger) : base(
            context,
            environment,
            logger)
        {
        }
    }
}
