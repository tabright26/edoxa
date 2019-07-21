// Filename: TestCashierModule.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Cashier.Api.Infrastructure;
using eDoxa.Cashier.IntegrationTests.Helpers.Mocks;
using eDoxa.IntegrationEvents;

using JetBrains.Annotations;

namespace eDoxa.Cashier.IntegrationTests.Helpers
{
    internal sealed class TestCashierModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
            builder.RegisterModule<CashierModule>();

            builder.RegisterType<MockIntegrationEventService>().As<IIntegrationEventService>().InstancePerDependency();
        }
    }
}
