// Filename: GamesModule.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Games.Api.Application.Factories;
using eDoxa.Games.Api.Application.Services;
using eDoxa.Games.Api.Infrastructure.Data;
using eDoxa.Games.Domain.Factories;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.Domain.Services;
using eDoxa.Games.Infrastructure.Repositories;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

namespace eDoxa.Games.Api.Infrastructure
{
    internal sealed class GamesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Cleaner
            builder.RegisterType<GamesDbContextCleaner>().As<IDbContextCleaner>().InstancePerLifetimeScope();

            // Seeder
            builder.RegisterType<GamesDbContextSeeder>().As<IDbContextSeeder>().InstancePerLifetimeScope();

            // Repositories
            builder.RegisterType<GameCredentialRepository>().As<IGameCredentialRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GameAuthenticationRepository>().As<IGameAuthenticationRepository>().SingleInstance();

            // Services
            builder.RegisterType<GameCredentialService>().As<IGameCredentialService>().InstancePerLifetimeScope();
            builder.RegisterType<GameAuthenticationService>().As<IGameAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<ChallengeService>().As<IChallengeService>().InstancePerLifetimeScope();

            // Factories
            builder.RegisterType<GameGameAuthenticationGeneratorFactory>().As<IGameAuthenticationGeneratorFactory>().SingleInstance();
            builder.RegisterType<GameGameAuthenticationValidatorFactory>().As<IGameAuthenticationValidatorFactory>().SingleInstance();
            builder.RegisterType<ChallengeMatchesFactory>().As<IChallengeMatchesFactory>().SingleInstance();

            // Games
            builder.RegisterModule<LeagueOfLegendsModule>();
        }
    }
}
