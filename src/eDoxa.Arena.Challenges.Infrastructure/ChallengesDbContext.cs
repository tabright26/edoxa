﻿// Filename: ChallengesDbContext.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.IO;
using System.Reflection;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Configurations;
using eDoxa.Seedwork.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Factories;

using JetBrains.Annotations;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure
{
    public sealed class ChallengesDbContext : CustomDbContext
    {
        public ChallengesDbContext(DbContextOptions<ChallengesDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        public DbSet<Challenge> Challenges => this.Set<Challenge>();

        public DbSet<Participant> Participants => this.Set<Participant>();

        public DbSet<Match> Matches => this.Set<Match>();

        protected override void OnModelCreating([NotNull] ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(nameof(eDoxa).ToLower());

            builder.ApplyConfiguration(new ChallengeConfiguration());

            builder.ApplyConfiguration(new ParticipantConfiguration());

            builder.ApplyConfiguration(new MatchConfiguration());
        }

        private sealed class ChallengesDbContextFactory : DesignTimeDbContextFactory<ChallengesDbContext>
        {
            protected override string BasePath => Path.Combine(Directory.GetCurrentDirectory(), "../eDoxa.Arena.Challenges.Api");

            protected override Assembly MigrationsAssembly => Assembly.GetAssembly(typeof(ChallengesDbContextFactory));

            [NotNull]
            public override ChallengesDbContext CreateDbContext(string[] args)
            {
                return new ChallengesDbContext(Options, new NoMediator());
            }
        }
    }
}
