// Filename: IntegrationEventModule.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Data.Common;

using Autofac;

using eDoxa.Seedwork.ServiceBus.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Seedwork.ServiceBus.Modules
{
    public sealed class IntegrationEventModule<TDbContext> : Module
    where TDbContext : DbContext
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
            builder.Register<Func<DbConnection, IIntegrationEventRepository>>(context => connection => new IntegrationEventRepository(connection));
            builder.RegisterType<IntegrationEventPublisher<TDbContext>>().As<IIntegrationEventPublisher>().InstancePerDependency();
        }
    }
}
