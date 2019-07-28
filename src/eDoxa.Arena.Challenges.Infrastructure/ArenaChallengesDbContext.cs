﻿// Filename: ChallengesDbContext.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Infrastructure.Configurations;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure
{
    public sealed class ArenaChallengesDbContext : DbContext
    {
        public ArenaChallengesDbContext(DbContextOptions<ArenaChallengesDbContext> options) : base(options)
        {
        }

        public DbSet<ChallengeModel> Challenges => this.Set<ChallengeModel>();

        public DbSet<ParticipantModel> Participants => this.Set<ParticipantModel>();

        public DbSet<MatchModel> Matches => this.Set<MatchModel>();

        protected override void OnModelCreating( ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ChallengeModelConfiguration());

            builder.ApplyConfiguration(new ParticipantModelConfiguration());

            builder.ApplyConfiguration(new MatchModelConfiguration());
        }
    }
}
