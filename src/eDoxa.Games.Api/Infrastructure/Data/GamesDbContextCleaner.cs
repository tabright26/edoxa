// Filename: GamesDbContextCleaner.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Games.Api.Infrastructure.Data
{
    internal sealed class GamesDbContextCleaner : DbContextCleaner
    {
        public GamesDbContextCleaner(GamesDbContext context, IWebHostEnvironment environment, ILogger<GamesDbContextCleaner> logger) : base(
            context,
            environment,
            logger)
        {
            Credentials = context.Set<Credential>();
        }

        private DbSet<Credential> Credentials { get; }

        protected override void Cleanup()
        {
            Credentials.RemoveRange(Credentials);
        }
    }
}
