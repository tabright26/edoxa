// Filename: RequestModule.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Seedwork.Application.Services;
using eDoxa.Seedwork.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Repositories;

namespace eDoxa.Autofac
{
    internal sealed class RequestModule<TContext> : Module
    where TContext : CustomDbContext
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestLogRepository<TContext>>().As<IRequestLogRepository>().InstancePerLifetimeScope();

            builder.RegisterType<RequestLogService>().As<IRequestLogService>().InstancePerLifetimeScope();
        }
    }
}