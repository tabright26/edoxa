// Filename: ChallengesModule.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;
using eDoxa.Autofac;
using eDoxa.Challenges.Application.Queries;
using eDoxa.Challenges.Application.Services;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Repositories;

namespace eDoxa.Challenges.Application
{
    public sealed class ApplicationModule : CustomServiceModule<ApplicationModule, ChallengesDbContext>
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<MediatorModule<ApplicationModule>>();

            // Repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ChallengeRepository>().As<IChallengeRepository>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<ChallengeQueries>().As<IChallengeQueries>().InstancePerLifetimeScope();

            builder.RegisterType<ParticipantQueries>().As<IParticipantQueries>().InstancePerLifetimeScope();

            builder.RegisterType<MatchQueries>().As<IMatchQueries>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<ChallengeDailyPublisherService>().As<IChallengeDailyPublisherService>().InstancePerLifetimeScope();

            builder.RegisterType<ChallengeWeeklyPublisherService>().As<IChallengeWeeklyPublisherService>().InstancePerLifetimeScope();

            builder.RegisterType<ChallengeMonthlyPublisherService>().As<IChallengeMonthlyPublisherService>().InstancePerLifetimeScope();

            builder.RegisterType<ChallengeSynchronizerService>().As<IChallengeSynchronizerService>().InstancePerLifetimeScope();

            builder.RegisterType<ChallengeCloserService>().As<IChallengeCloserService>().InstancePerLifetimeScope();
        }
    }
}