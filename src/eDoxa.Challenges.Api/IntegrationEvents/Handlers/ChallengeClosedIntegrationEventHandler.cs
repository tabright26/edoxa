// Filename: ChallengeClosedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.IntegrationEvents.Handlers
{
    public sealed class ChallengeClosedIntegrationEventHandler : IIntegrationEventHandler<ChallengeClosedIntegrationEvent>
    {
        public Task HandleAsync(ChallengeClosedIntegrationEvent integrationEvent)
        {
            throw new NotImplementedException();
        }
    }
}
