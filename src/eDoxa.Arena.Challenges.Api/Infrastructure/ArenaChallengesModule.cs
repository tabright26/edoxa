// Filename: ArenaChallengesModule.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Challenges.Api.Application.Adapters;
using eDoxa.Arena.Challenges.Api.Application.DomainEvents;
using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Api.Application.Services;
using eDoxa.Arena.Challenges.Api.Application.Strategies;
using eDoxa.Arena.Challenges.Api.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Domain.Strategies;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Commands;
using eDoxa.IntegrationEvents;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Infrastructure
{
    public sealed class ArenaChallengesModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterModule<DomainEventModule>();
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<IntegrationEventModule<ChallengesDbContext>>();

            // Repositories
            builder.RegisterType<ChallengeRepository>().As<IChallengeRepository>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<ChallengeQuery>().As<IChallengeQuery>().InstancePerLifetimeScope();
            builder.RegisterType<ParticipantQuery>().As<IParticipantQuery>().InstancePerLifetimeScope();
            builder.RegisterType<MatchQuery>().As<IMatchQuery>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<ChallengeService>().As<IChallengeService>().InstancePerLifetimeScope();

            // Strategies
            builder.RegisterType<LeagueOfLegendsScoringStrategy>().As<IScoringStrategy>();

            // Adapters
            builder.RegisterType<LeagueOfLegendsGameReferencesAdapter>().As<IGameReferencesAdapter>();
            builder.RegisterType<LeagueOfLegendsMatchAdapter>().As<IMatchAdapter>();

            // Factories
            builder.RegisterType<ScoringFactory>().As<IScoringFactory>().SingleInstance();
            builder.RegisterType<GameReferencesFactory>().As<IGameReferencesFactory>().SingleInstance();
            builder.RegisterType<MatchFactory>().As<IMatchFactory>().SingleInstance();
        }
    }
}
