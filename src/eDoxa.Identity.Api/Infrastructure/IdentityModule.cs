// Filename: IdentityModule.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.IntegrationEvents;
using eDoxa.Seedwork.Application.DomainEvents;

using JetBrains.Annotations;

namespace eDoxa.Identity.Api.Infrastructure
{
    public sealed class IdentityModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterModule<DomainEventModule>();
            builder.RegisterModule<IntegrationEventModule<IdentityDbContext>>();
        }
    }
}
