// Filename: MockIntegrationEventLogEntry.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.IntegrationEvents.UnitTests.Mocks
{
    internal sealed class MockIntegrationEventLogEntry : IntegrationEventLogEntry
    {
        public MockIntegrationEventLogEntry(IntegrationEvent integrationEvent) : base(integrationEvent)
        {
        }
    }
}
