// Filename: GamesModule.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Games.Api.Areas.Games.Services;
using eDoxa.Arena.Games.Domain.Repositories;
using eDoxa.Arena.Games.Domain.Services;
using eDoxa.Arena.Games.Infrastructure.Repositories;

namespace eDoxa.Arena.Games.Api.Infrastructure
{
    internal sealed class GamesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repositories
            builder.RegisterType<GameCredentialRepository>().As<IGameCredentialRepository>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<GameCredentialService>().As<IGameCredentialService>().InstancePerLifetimeScope();
            builder.RegisterType<GameService>().As<IGameService>().InstancePerDependency();
        }
    }
}
