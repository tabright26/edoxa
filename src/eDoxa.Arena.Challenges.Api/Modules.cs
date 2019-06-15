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

using eDoxa.Arena.Challenges.Api.Application.Abstractions;
using eDoxa.Arena.Challenges.Api.Application.Queries;
using eDoxa.Arena.Challenges.Domain.Abstractions.Factories;
using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.Abstractions.Services;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Commands;
using eDoxa.IntegrationEvents;
using eDoxa.Seedwork.Application.DomainEvents;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api
{
    public sealed class Modules : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<DomainEventModule>();

            builder.RegisterModule<CommandModule>();

            builder.RegisterModule<IntegrationEventModule<ChallengesDbContext>>();

            // Repositories
            builder.RegisterType<ChallengeRepository>().As<IChallengeRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ParticipantRepository>().As<IParticipantRepository>().InstancePerLifetimeScope();

            builder.RegisterType<MatchRepository>().As<IMatchRepository>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<ChallengeQuery>().As<IChallengeQuery>().InstancePerLifetimeScope();

            builder.RegisterType<ParticipantQuery>().As<IParticipantQuery>().InstancePerLifetimeScope();

            builder.RegisterType<MatchQuery>().As<IMatchQuery>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<ChallengeService>().As<IChallengeService>().InstancePerLifetimeScope();

            // Factories
            builder.RegisterType<MatchReferencesFactory>().As<IMatchReferencesFactory>().SingleInstance();

            builder.RegisterType<MatchStatsFactory>().As<IMatchStatsFactory>().SingleInstance();
        }
    }
}
