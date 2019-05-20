// Filename: ChallengesDbContext.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.Infrastructure.Configurations;
using eDoxa.Seedwork.Infrastructure;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Infrastructure
{
    public sealed partial class ChallengesDbContext
    {
        private static readonly FakeRandomChallengeFactory FakeRandomChallengeFactory = FakeRandomChallengeFactory.Instance;

        public async Task SeedAsync(ILogger logger)
        {
            if (!Challenges.Any())
            {
                var challenges = FakeRandomChallengeFactory.CreateRandomChallengesWithOtherStates(ChallengeState.Opened);

                Challenges.AddRange(challenges);

                await this.SaveChangesAsync();

                logger.LogInformation("The challenges being populated.");
            }
            else
            {
                logger.LogInformation("The challenges already populated.");
            }
        }
    }

    public sealed partial class ChallengesDbContext : CustomDbContext
    {
        public ChallengesDbContext(DbContextOptions<ChallengesDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        internal ChallengesDbContext(DbContextOptions<ChallengesDbContext> options) : base(options)
        {
        }

        public DbSet<Challenge> Challenges => this.Set<Challenge>();

        public DbSet<Participant> Participants => this.Set<Participant>();

        public DbSet<Match> Matches => this.Set<Match>();

        public DbSet<Stat> Stats => this.Set<Stat>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(nameof(eDoxa).ToLower());

            builder.ApplyConfiguration(new ChallengeConfiguration());

            builder.ApplyConfiguration(new ParticipantConfiguration());

            builder.ApplyConfiguration(new MatchConfiguration());

            builder.ApplyConfiguration(new StatConfiguration());
        }
    }
}