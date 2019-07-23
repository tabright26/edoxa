// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider Build<TModule>(this IServiceCollection services)
        where TModule : IModule, new()
        {
            // Create an Autofac container builder instance.
            var builder = new ContainerBuilder();

            // Register the Autofac module for a specific microservices.
            builder.RegisterModule<TModule>();

            // Populates the Autofac container builder with the set of registered service descriptors.
            builder.Populate(services);

            // Create an Autofac service provider instance.
            return new AutofacServiceProvider(builder.Build());
        }
    }
}
