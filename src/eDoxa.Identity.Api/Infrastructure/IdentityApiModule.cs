// Filename: IdentityApiModule.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Data;
using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Seedwork.Infrastructure;

namespace eDoxa.Identity.Api.Infrastructure
{
    internal sealed class IdentityApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Storage
            builder.RegisterType<IdentityFileStorage>().As<IIdentityFileStorage>().InstancePerDependency();
            builder.RegisterType<IdentityTestFileStorage>().As<IIdentityTestFileStorage>().InstancePerDependency();

            // Seeder
            builder.RegisterType<IdentityDbContextSeeder>().As<IDbContextSeeder>().InstancePerLifetimeScope();

            // Cleaner
            builder.RegisterType<IdentityDbContextCleaner>().As<IDbContextCleaner>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<RedirectService>().As<IRedirectService>().InstancePerDependency();
        }
    }
}
