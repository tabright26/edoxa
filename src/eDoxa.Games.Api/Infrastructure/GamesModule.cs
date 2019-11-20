// Filename: GamesModule.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Games.Abstractions.Factories;
using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.Factories;
using eDoxa.Games.Infrastructure.Repositories;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.Services;

namespace eDoxa.Games.Api.Infrastructure
{
    internal sealed class GamesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
            builder.RegisterType<ChallengeScoringFactory>().As<IChallengeScoringFactory>().SingleInstance();
            builder.RegisterType<ChallengeMatchesFactory>().As<IChallengeMatchesFactory>().SingleInstance();

            // Games
            builder.RegisterModule<LeagueOfLegendsModule>();
        }
    }
}
