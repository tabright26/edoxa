// Filename: MockDynamicIntegrationEventHandler2.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

namespace eDoxa.ServiceBus.Tests.Mocks
{
    internal sealed class MockDynamicIntegrationEventHandler2 : IDynamicIntegrationEventHandler
    {
        public MockDynamicIntegrationEventHandler2()
        {
            Handled = false;
        }

        public async Task Handle(dynamic integrationEvent)
        {
            Handled = true;

            await Task.CompletedTask;
        }

        public bool Handled { get; private set; }
    }
}