// Filename: IdentityApiModule.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Identity.Api.Areas.Identity.Services;

namespace eDoxa.Identity.Api.Infrastructure
{
    internal sealed class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Services
            builder.RegisterType<RedirectService>().As<IRedirectService>().InstancePerDependency();
        }
    }
}
