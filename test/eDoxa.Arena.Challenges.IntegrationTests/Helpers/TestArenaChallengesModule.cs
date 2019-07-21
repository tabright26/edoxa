// Filename: TestArenaChallengesModule.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Arena.Challenges.Api.Infrastructure;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers.Mocks;
using eDoxa.IntegrationEvents;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers
{
    internal sealed class TestArenaChallengesModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterModule<ArenaChallengesModule>();

            builder.RegisterType<MockIntegrationEventService>().As<IIntegrationEventService>().InstancePerDependency();
        }
    }
}
