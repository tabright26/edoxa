// Filename: Modules.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Arena.Challenges.Api.Application.Adapters;
using eDoxa.Arena.Challenges.Api.Application.DomainEvents;
using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Api.Application.Queries;
using eDoxa.Arena.Challenges.Api.Application.Services;
using eDoxa.Arena.Challenges.Api.Application.Strategies;
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
    public sealed class ApiModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

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
            builder.RegisterType<PayoutStrategy>().As<IPayoutStrategy>();
            builder.RegisterType<LeagueOfLegendsScoringStrategy>().As<IScoringStrategy>();

            // Adapters
            builder.RegisterType<LeagueOfLegendsGameReferencesAdapter>().As<IGameReferencesAdapter>();
            builder.RegisterType<LeagueOfLegendsMatchStatsAdapter>().As<IMatchStatsAdapter>();

            // Factories
            builder.RegisterType<PayoutFactory>().As<IPayoutFactory>().SingleInstance();
            builder.RegisterType<ScoringFactory>().As<IScoringFactory>().SingleInstance();
            builder.RegisterType<GameReferencesFactory>().As<IGameReferencesFactory>().SingleInstance();
            builder.RegisterType<MatchStatsFactory>().As<IMatchStatsFactory>().SingleInstance();
        }
    }
}
