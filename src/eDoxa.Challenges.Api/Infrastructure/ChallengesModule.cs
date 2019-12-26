// Filename: ChallengesModule.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Challenges.Api.Application.Services;
using eDoxa.Challenges.Api.Infrastructure.Data;
using eDoxa.Challenges.Api.Infrastructure.Queries;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Challenges.Infrastructure.Repositories;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

namespace eDoxa.Challenges.Api.Infrastructure
{
    internal sealed class ChallengesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Cleaner
            builder.RegisterType<ChallengesDbContextCleaner>().As<IDbContextCleaner>().InstancePerLifetimeScope();

            // Seeder
            builder.RegisterType<ChallengesDbContextSeeder>().As<IDbContextSeeder>().InstancePerLifetimeScope();

            // Repositories
            builder.RegisterType<ChallengeRepository>().As<IChallengeRepository>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<ChallengeQuery>().As<IChallengeQuery>().InstancePerLifetimeScope();
            builder.RegisterType<ParticipantQuery>().As<IParticipantQuery>().InstancePerLifetimeScope();
            builder.RegisterType<MatchQuery>().As<IMatchQuery>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<ChallengeService>().As<IChallengeService>().InstancePerLifetimeScope();
        }
    }
}
