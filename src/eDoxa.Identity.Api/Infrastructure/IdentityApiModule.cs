// Filename: IdentityApiModule.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Identity.Api.Infrastructure.Data;
using eDoxa.Seedwork.Infrastructure;

namespace eDoxa.Identity.Api.Infrastructure
{
    public sealed class IdentityApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Seeder
            builder.RegisterType<IdentityDbContextSeeder>().As<IDbContextSeeder>().InstancePerLifetimeScope();
        }
    }
}
