// Filename: MockIntegrationEventService.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.IntegrationEvents;

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers.Mocks
{
    public sealed class MockIntegrationEventService : IIntegrationEventService
    {
        public async Task PublishAsync(IntegrationEvent integrationEvent)
        {
            await Task.CompletedTask;
        }
    }
}
