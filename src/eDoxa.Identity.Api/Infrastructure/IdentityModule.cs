// Filename: IdentityApiModule.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.Infrastructure.Data;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.Infrastructure.Repositories;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

namespace eDoxa.Identity.Api.Infrastructure
{
    internal sealed class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Cleaner
            builder.RegisterType<IdentityDbContextCleaner>().As<IDbContextCleaner>().InstancePerLifetimeScope();

            // Seeder
            builder.RegisterType<IdentityDbContextSeeder>().As<IDbContextSeeder>().InstancePerLifetimeScope();

            // Reposiotries
            builder.RegisterType<AddressRepository>().As<IAddressRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DoxatagRepository>().As<IDoxatagRepository>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<AddressService>().As<IAddressService>().InstancePerLifetimeScope();
            builder.RegisterType<DoxatagService>().As<IDoxatagService>().InstancePerLifetimeScope();
            builder.RegisterType<RedirectService>().As<IRedirectService>().InstancePerDependency();
        }
    }
}
