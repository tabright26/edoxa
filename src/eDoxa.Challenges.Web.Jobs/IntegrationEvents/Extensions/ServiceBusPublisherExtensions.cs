// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Web.Jobs.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishChallengesSynchronizedIntegrationEventAsync(this IServiceBusPublisher publisher, Game game)
        {
            await publisher.PublishAsync(new ChallengesSynchronizedIntegrationEvent(game));
        }
    }
}
