// Filename: ArenaChallengesApiModule.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Challenges.Api.Application.Adapters;
using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Api.Application.Services;
using eDoxa.Arena.Challenges.Api.Application.Strategies;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data;
using eDoxa.Arena.Challenges.Api.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Domain.Strategies;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Seedwork.Infrastructure;

namespace eDoxa.Arena.Challenges.Api.Infrastructure
{
    public sealed class ArenaChallengesApiModule : Module
    {
        protected override void Load( ContainerBuilder builder)
        {
            // Seeder
            builder.RegisterType<ArenaChallengesDbContextSeeder>().As<IDbContextSeeder>().InstancePerLifetimeScope();

            // Repositories
            builder.RegisterType<ChallengeRepository>().As<IChallengeRepository>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<ChallengeQuery>().As<IChallengeQuery>().InstancePerLifetimeScope();
            builder.RegisterType<ParticipantQuery>().As<IParticipantQuery>().InstancePerLifetimeScope();
            builder.RegisterType<MatchQuery>().As<IMatchQuery>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<ChallengeService>().As<IChallengeService>().InstancePerLifetimeScope();

            // Strategies
            builder.RegisterType<LeagueOfLegendsScoringStrategy>().As<IScoringStrategy>().SingleInstance();

            // Adapters
            builder.RegisterType<LeagueOfLegendsGameReferencesAdapter>().As<IGameReferencesAdapter>().SingleInstance();
            builder.RegisterType<LeagueOfLegendsMatchAdapter>().As<IMatchAdapter>().SingleInstance();

            // Factories
            builder.RegisterType<ScoringFactory>().As<IScoringFactory>().SingleInstance();
            builder.RegisterType<GameReferencesFactory>().As<IGameReferencesFactory>().SingleInstance();
            builder.RegisterType<MatchFactory>().As<IMatchFactory>().SingleInstance();
        }
    }
}
