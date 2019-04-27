﻿// Filename: CustomServiceModule.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Seedwork.Infrastructure;
using JetBrains.Annotations;

namespace eDoxa.Autofac
{
    public abstract class CustomServiceModule<TStartup, TContext> : Module
    where TContext : CustomDbContext
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterModule<RequestModule<TContext>>();
            builder.RegisterModule<IntegrationEventModule<TStartup, TContext>>();
        }
    }
}