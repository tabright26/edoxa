﻿// Filename: Modules.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Arena.Challenges.Application.Abstractions.Queries;
using eDoxa.Arena.Challenges.Application.Queries;
using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.Services;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Arena.Challenges.Services.Factories;
using eDoxa.Seedwork.Application.Commands;
using eDoxa.Seedwork.Application.DomainEvents;
using eDoxa.ServiceBus;

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

            builder.RegisterType<PayoutFactory>().As<IPayoutFactory>().SingleInstance();

            builder.RegisterType<ScoringFactory>().As<IScoringFactory>().SingleInstance();
        }
    }
}
