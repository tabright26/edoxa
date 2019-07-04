// Filename: TestModule.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Cashier.Api.Infrastructure;
using eDoxa.Cashier.IntegrationTests.Helpers.Mocks;
using eDoxa.IntegrationEvents;

using JetBrains.Annotations;

namespace eDoxa.Cashier.IntegrationTests.Helpers
{
    internal sealed class TestModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterModule<ApiModule>();

            builder.RegisterType<MockIntegrationEventService>().As<IIntegrationEventService>().InstancePerDependency();
        }
    }
}
