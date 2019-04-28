// Filename: CommandInfrastructureModule.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Commands.Infrastructure.Repositories;
using eDoxa.Commands.Services;
using eDoxa.Seedwork.Infrastructure;

using JetBrains.Annotations;

namespace eDoxa.Commands
{
    public sealed class CommandInfrastructureModule<TDbContext> : Module
    where TDbContext : CustomDbContext
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            // Repositories
            builder.RegisterType<CommandRepository<TDbContext>>().As<ICommandRepository>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<CommandService>().As<ICommandService>().InstancePerLifetimeScope();
        }
    }
}