// Filename: GamesModule.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Games.Abstractions.Factories;
using eDoxa.Arena.Games.Abstractions.Services;
using eDoxa.Arena.Games.Domain.Repositories;
using eDoxa.Arena.Games.Factories;
using eDoxa.Arena.Games.Infrastructure.Repositories;
using eDoxa.Arena.Games.LeagueOfLegends;
using eDoxa.Arena.Games.Services;

namespace eDoxa.Arena.Games.Api.Infrastructure
{
    internal sealed class GamesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repositories
            builder.RegisterType<CredentialRepository>().As<ICredentialRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthFactorRepository>().As<IAuthFactorRepository>().SingleInstance();

            // Services
            builder.RegisterType<CredentialService>().As<ICredentialService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthFactorService>().As<IAuthFactorService>().InstancePerLifetimeScope();

            // Factories
            builder.RegisterType<AuthFactorGeneratorFactory>().As<IAuthFactorGeneratorFactory>().SingleInstance();
            builder.RegisterType<AuthFactorValidatorFactory>().As<IAuthFactorValidatorFactory>().SingleInstance();

            // Games
            builder.RegisterModule<LeagueOfLegendsModule>();
        }
    }
}
