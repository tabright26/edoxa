// Filename: ChallengeService.cs
// Date Created: 2019-11-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Worker.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Worker.Application.RecurringJobs
{
    public sealed class ChallengeRecurringJob
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ChallengeRecurringJob(IServiceBusPublisher serviceBusPublisher)
        {
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task SynchronizeChallengeAsync(Game game)
        {
            await _serviceBusPublisher.PublishChallengesSynchronizedIntegrationEventAsync(game);
        }
    }
}
