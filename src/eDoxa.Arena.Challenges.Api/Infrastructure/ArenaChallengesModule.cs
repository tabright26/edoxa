// Filename: ArenaChallengesApiModule.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Adapters;
using eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories;
using eDoxa.Arena.Challenges.Api.Areas.Challenges.Services;
using eDoxa.Arena.Challenges.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Arena.Challenges.Api.Areas.Challenges.Strategies;
using eDoxa.Arena.Challenges.Api.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Strategies;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;

namespace eDoxa.Arena.Challenges.Api.Infrastructure
{
    internal sealed class ArenaChallengesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
