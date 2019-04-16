// Filename: IntegrationEventModule.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Data.Common;

using Autofac;

using eDoxa.ServiceBus;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Autofac
{
    public sealed class IntegrationEventModule<TStartup, TContext> : Module
    where TContext : DbContext
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            // Register all the CommandHandler classes (they implement IIntegrationEventHandler) in assembly holding the CommandHandlers.
            builder.RegisterAssemblyTypes(typeof(TStartup).Assembly).AsClosedTypesOf(
                typeof(IIntegrationEventHandler<>)
            );

            // Register the IntegrationEventLogger.
            builder.Register<Func<DbConnection, IIntegrationEventLogRepository>>(
                context => connection => new IntegrationEventLogRepository(connection)
            );

            builder.RegisterType<IntegrationEventService<TContext>>().As<IIntegrationEventService>().InstancePerDependency();
        }
    }
}