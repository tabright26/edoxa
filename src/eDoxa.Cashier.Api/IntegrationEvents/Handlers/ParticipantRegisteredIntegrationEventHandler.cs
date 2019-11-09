// Filename: ParticipantRegisteredIntegrationEventHandler.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class ParticipantRegisteredIntegrationEventHandler : IIntegrationEventHandler<ParticipantRegisteredIntegrationEvent>
    {
        public Task HandleAsync(ParticipantRegisteredIntegrationEvent integrationEvent)
        {
            return Task.CompletedTask;
        }
    }
}
