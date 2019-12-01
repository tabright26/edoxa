// Filename: ChallengeService.cs
// Date Created: 2019-11-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Web.Jobs.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Web.Jobs.RecurringJobs
{
    public sealed class SynchronizeChallengesRecurringJob
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public SynchronizeChallengesRecurringJob(IServiceBusPublisher serviceBusPublisher)
        {
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task ExecuteAsync(Game game)
        {
            await _serviceBusPublisher.PublishChallengesSynchronizedIntegrationEventAsync(game);
        }
    }
}
