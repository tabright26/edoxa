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

using eDoxa.Identity.Application.Abstractions.Queries;
using eDoxa.Identity.Application.Queries;
using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Application.Commands;
using eDoxa.Seedwork.Application.DomainEvents;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Identity.Api
{
    public sealed class Modules : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<DomainEventModule>();

            builder.RegisterModule<CommandModule>();

            builder.RegisterModule<IntegrationEventModule<IdentityDbContext>>();

            // Queries
            builder.RegisterType<UserQuery>().As<IUserQuery>().InstancePerLifetimeScope();
        }
    }
}