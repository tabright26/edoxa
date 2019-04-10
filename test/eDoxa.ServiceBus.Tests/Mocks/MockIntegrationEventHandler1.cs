// Filename: MockIntegrationEventHandler1.cs
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
    internal sealed class MockIntegrationEventHandler1 : IIntegrationEventHandler<MockIntegrationEvent1>
    {
        public MockIntegrationEventHandler1()
        {
            Handled = false;
        }

        public Task Handle(MockIntegrationEvent1 integrationEvent1)
        {
            Handled = true;

            return Task.CompletedTask;
        }

        public bool Handled { get; private set; }
    }
}