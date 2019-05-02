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
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Identity.DTO.Queries;
using eDoxa.Identity.Infrastructure;
using eDoxa.Identity.Infrastructure.Repositories;
using eDoxa.Seedwork.Application;
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

            // Repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();

            // Queries
            builder.RegisterType<UserQueries>().As<IUserQueries>().InstancePerLifetimeScope();
        }
    }
}