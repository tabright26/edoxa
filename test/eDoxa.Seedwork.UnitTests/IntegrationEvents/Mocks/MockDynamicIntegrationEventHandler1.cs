// Filename: MockDynamicIntegrationEventHandler1.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Seedwork.ServiceBus;

namespace eDoxa.Seedwork.UnitTests.IntegrationEvents.Mocks
{
    internal sealed class MockDynamicIntegrationEventHandler1 : IDynamicIntegrationEventHandler
    {
        public MockDynamicIntegrationEventHandler1()
        {
            Handled = false;
        }

        public bool Handled { get; private set; }

        public async Task HandleAsync(dynamic integrationEvent)
        {
            Handled = true;

            await Task.CompletedTask;
        }
    }
}
