// Filename: IdentityApiModule.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Identity.Infrastructure.Repositories;

namespace eDoxa.Identity.Api.Infrastructure
{
    internal sealed class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
