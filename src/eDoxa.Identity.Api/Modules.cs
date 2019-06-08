// Filename: Modules.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Identity.Api.Application.Abstractions;
using eDoxa.Identity.Api.Application.Queries;
using eDoxa.Identity.Infrastructure;
using eDoxa.IntegrationEvents;
using eDoxa.Seedwork.Application.DomainEvents;

using JetBrains.Annotations;

namespace eDoxa.Identity.Api
{
    public sealed class Modules : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<DomainEventModule>();

            builder.RegisterModule<IntegrationEventModule<IdentityDbContext>>();

            // Queries
            builder.RegisterType<UserQuery>().As<IUserQuery>().InstancePerLifetimeScope();
        }
    }
}
