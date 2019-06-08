// Filename: MockIntegrationEventHandler2.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

namespace eDoxa.IntegrationEvents.UnitTests.Mocks
{
    internal sealed class MockIntegrationEventHandler2 : IIntegrationEventHandler<MockIntegrationEvent1>
    {
        public MockIntegrationEventHandler2()
        {
            Handled = false;
        }

        public bool Handled { get; private set; }

        public Task Handle(MockIntegrationEvent1 integrationEvent1)
        {
            Handled = true;

            return Task.CompletedTask;
        }
    }
}
