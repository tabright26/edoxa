// Filename: MockIntegrationEvent2.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using JetBrains.Annotations;

namespace eDoxa.ServiceBus.Tests.Mocks
{
    internal sealed class MockIntegrationEvent2 : MockIntegrationEvent
    {
        [CanBeNull]
        public MockIntegrationEvent2 Clone()
        {
            return this.MemberwiseClone() as MockIntegrationEvent2;
        }
    }
}