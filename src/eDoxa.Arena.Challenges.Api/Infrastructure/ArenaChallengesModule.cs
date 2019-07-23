// Filename: ArenaChallengesModule.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Challenges.Api.Application.DomainEvents;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Commands;
using eDoxa.IntegrationEvents;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Infrastructure
{
    public sealed class ArenaChallengesModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterModule<DomainEventModule>();
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<IntegrationEventModule<ArenaChallengesDbContext>>();
        }
    }
}
