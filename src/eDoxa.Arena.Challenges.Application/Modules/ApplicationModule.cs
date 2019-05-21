// Filename: ApplicationModule.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Arena.Challenges.Application.Queries;
using eDoxa.Arena.Challenges.Application.Services;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Modules;
using eDoxa.Commands;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Application.Modules
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

            builder.RegisterModule<InfrastructureModule>();

            // Queries
            builder.RegisterType<ChallengeQuery>().As<IChallengeQuery>().InstancePerLifetimeScope();

            builder.RegisterType<ParticipantQuery>().As<IParticipantQuery>().InstancePerLifetimeScope();

            builder.RegisterType<MatchQuery>().As<IMatchQuery>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<ChallengeService>().As<IChallengeService>().InstancePerLifetimeScope();
        }
    }
}
