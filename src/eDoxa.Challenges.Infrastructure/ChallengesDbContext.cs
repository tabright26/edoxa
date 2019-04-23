// Filename: ChallengesDbContext.cs
// Date Created: 2019-04-07
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
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
        private static readonly UserAggregateFactory UserAggregateFactory = UserAggregateFactory.Instance;
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;        

        public async Task SeedAsync(ILogger logger)
        {
            if (!Users.Any())
            {
                var admin = UserAggregateFactory.CreateAdmin();

                Users.Add(admin);

                await this.SaveChangesAsync();

                logger.LogInformation("The users being populated.");
            }
            else
            {
                logger.LogInformation("The users already populated.");
            }

            if (!Challenges.Any())
            {
                var challenges = ChallengeAggregateFactory.CreateRandomChallengesWithOtherStates(ChallengeState1.Opened);

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

        public DbSet<User> Users
        {
            get
            {
                return this.Set<User>();
            }
        }

        public DbSet<Challenge> Challenges
        {
            get
            {
                return this.Set<Challenge>();
            }
        }

        public DbSet<Participant> Participants
        {
            get
            {
                return this.Set<Participant>();
            }
        }

        public DbSet<Match> Matches
        {
            get
            {
                return this.Set<Match>();
            }
        }

        public DbSet<Stat> Stats
        {
            get
            {
                return this.Set<Stat>();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(nameof(eDoxa).ToLower());

            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new ChallengeConfiguration());

            builder.ApplyConfiguration(new ParticipantConfiguration());

            builder.ApplyConfiguration(new MatchConfiguration());

            builder.ApplyConfiguration(new StatConfiguration());
        }
    }
}