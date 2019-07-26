// Filename: MockIntegrationEventService.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.IntegrationEvents;

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
