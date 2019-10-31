// Filename: GamesDbContextSeeder.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Games.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Arena.Games.Api.Infrastructure.Data
{
    internal sealed class GamesDbContextSeeder : DbContextSeeder
    {
        private readonly GamesDbContext _context;

        public GamesDbContextSeeder(GamesDbContext context, IHostingEnvironment environment, ILogger<GamesDbContextSeeder> logger) : base(environment, logger)
        {
            _context = context;
        }

        protected override Task SeedDevelopmentAsync()
        {
            //if (!_context.Challenges.Any())
            //{
            //    var challenges = FileStorage.Challenges;

            //    _challengeRepository.Create(challenges);

            //    await _challengeRepository.CommitAsync();
            //}

            return Task.CompletedTask;
        }
    }
}
