// Filename: IntegrationEventModule.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Data.Common;

using Autofac;

using eDoxa.Seedwork.IntegrationEvents.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Seedwork.IntegrationEvents
{
    public sealed class IntegrationEventModule<TDbContext> : Module
    where TDbContext : DbContext
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
            builder.Register<Func<DbConnection, IIntegrationEventLogRepository>>(context => connection => new IntegrationEventLogRepository(connection));
            builder.RegisterType<IntegrationEventService<TDbContext>>().As<IIntegrationEventService>().InstancePerDependency();
        }
    }
}
