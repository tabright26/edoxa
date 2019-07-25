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

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Seedwork.IntegrationEvents
{
    public sealed class IntegrationEventModule<TContext> : Module
    where TContext : DbContext
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
            builder.Register<Func<DbConnection, IIntegrationEventLogRepository>>(context => connection => new IntegrationEventLogRepository(connection));
            builder.RegisterType<IntegrationEventService<TContext>>().As<IIntegrationEventService>().InstancePerDependency();
        }
    }
}