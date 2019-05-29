// Filename: IdentityModule.cs
// Date Created: 2019-04-01
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Commands;
using eDoxa.Identity.Application.Queries;
using eDoxa.Identity.DTO.Queries;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Application.Modules;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Identity.Application
{
    public sealed class ApplicationModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<DomainEventModule<ApplicationModule>>();

            builder.RegisterModule<CommandModule<ApplicationModule>>();

            builder.RegisterModule<IntegrationEventModule<ApplicationModule, IdentityDbContext>>();

            // Queries
            builder.RegisterType<UserQueries>().As<IUserQueries>().InstancePerLifetimeScope();
        }
    }
}