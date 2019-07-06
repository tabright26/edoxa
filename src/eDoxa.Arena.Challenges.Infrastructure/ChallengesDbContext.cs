// Filename: ChallengesDbContext.cs
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
using eDoxa.Seedwork.Infrastructure;

using JetBrains.Annotations;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure
{
    public sealed class ChallengesDbContext : DbContext
    {
        public ChallengesDbContext(DbContextOptions<ChallengesDbContext> options, IMediator mediator) : base(options)
        {
            Mediator = mediator;
        }

        public ChallengesDbContext(DbContextOptions<ChallengesDbContext> options) : base(options)
        {
            Mediator = new FakeMediator();
        }

        public IMediator Mediator { get; }

        public DbSet<ChallengeModel> Challenges => this.Set<ChallengeModel>();

        public DbSet<ParticipantModel> Participants => this.Set<ParticipantModel>();

        public DbSet<MatchModel> Matches => this.Set<MatchModel>();

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ChallengeModelConfiguration());

            builder.ApplyConfiguration(new ParticipantModelConfiguration());

            builder.ApplyConfiguration(new MatchModelConfiguration());
        }
    }
}
