// Filename: MockIntegrationEventService.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.IntegrationEvents;

namespace eDoxa.Cashier.IntegrationTests.Helpers.Mocks
{
    public sealed class MockIntegrationEventService : IIntegrationEventService
    {
        public Task PublishAsync(IntegrationEvent integrationEvent)
        {
            return Task.CompletedTask;
        }
    }
}
