// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishChallengeClosedIntegrationEventAsync(this IServiceBusPublisher serviceBusPublisher, ChallengeId challengeId)
        {
            var integrationEvent = new ChallengeClosedIntegrationEvent
            {
                ChallengeId = challengeId
            };

            await serviceBusPublisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishCreateChallengePayoutFailedIntegrationEventAsync(this IServiceBusPublisher serviceBusPublisher, ChallengeId challengeId)
        {
            var integrationEvent = new CreateChallengePayoutFailedIntegrationEvent
            {
                ChallengeId = challengeId
            };

            await serviceBusPublisher.PublishAsync(integrationEvent);
        }
    }
}
