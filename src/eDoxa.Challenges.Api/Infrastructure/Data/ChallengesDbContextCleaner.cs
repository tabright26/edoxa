// Filename: ChallengesDbContextCleaner.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Api.Infrastructure.Data
{
    internal sealed class ChallengesDbContextCleaner : DbContextCleaner
    {
        public ChallengesDbContextCleaner(ChallengesDbContext context, IWebHostEnvironment environment, ILogger<ChallengesDbContextCleaner> logger) : base(
            context,
            environment,
            logger)
        {
            Challenges = context.Set<ChallengeModel>();
        }

        private DbSet<ChallengeModel> Challenges { get; }

        protected override void Cleanup()
        {
            Challenges.RemoveRange(Challenges);
        }
    }
}
