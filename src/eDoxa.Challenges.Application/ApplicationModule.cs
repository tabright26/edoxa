// Filename: ApplicationModule.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Challenges.Application.Queries;
using eDoxa.Challenges.Application.Services;
using eDoxa.Challenges.Domain.Entities.Repositories;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Repositories;
using eDoxa.Commands;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Application
{
    public sealed class ApplicationModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<DomainEventModule<ApplicationModule>>();

            builder.RegisterModule<CommandModule<ApplicationModule>>();

            builder.RegisterModule<CommandInfrastructureModule<ChallengesDbContext>>();

            builder.RegisterModule<IntegrationEventModule<ApplicationModule, ChallengesDbContext>>();

            // Repositories
            builder.RegisterType<ChallengeRepository>().As<IChallengeRepository>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<ChallengeQueries>().As<IChallengeQueries>().InstancePerLifetimeScope();

            builder.RegisterType<ParticipantQueries>().As<IParticipantQueries>().InstancePerLifetimeScope();

            builder.RegisterType<MatchQueries>().As<IMatchQueries>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<ChallengeService>().As<IChallengeService>().InstancePerLifetimeScope();
        }
    }
}