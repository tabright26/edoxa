﻿// Filename: MockIntegrationEvent1.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Seedwork.UnitTests.IntegrationEvents.Mocks
{
    internal sealed class MockIntegrationEvent1 : MockIntegrationEvent
    {
        public MockIntegrationEvent1? Clone()
        {
            return this.MemberwiseClone() as MockIntegrationEvent1;
        }
    }
}
