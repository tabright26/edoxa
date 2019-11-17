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
            builder.RegisterType<CredentialRepository>().As<ICredentialRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationRepository>().As<IAuthenticationRepository>().SingleInstance();

            // Services
            builder.RegisterType<CredentialService>().As<ICredentialService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<ChallengeService>().As<IChallengeService>().InstancePerLifetimeScope();

            // Factories
            builder.RegisterType<AuthenticationGeneratorFactory>().As<IAuthenticationGeneratorFactory>().SingleInstance();
            builder.RegisterType<AuthenticationValidatorFactory>().As<IAuthenticationValidatorFactory>().SingleInstance();
            builder.RegisterType<ChallengeScoringFactory>().As<IChallengeScoringFactory>().SingleInstance();
            builder.RegisterType<ChallengeMatchesFactory>().As<IChallengeMatchesFactory>().SingleInstance();

            // Games
            builder.RegisterModule<LeagueOfLegendsModule>();
        }
    }
}
