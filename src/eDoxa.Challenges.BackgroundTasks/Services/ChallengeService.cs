// Filename: ChallengeService.cs
// Date Created: 2019-11-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.BackgroundTasks.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.BackgroundTasks.Services
{
    public sealed class ChallengeService
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ChallengeService(IServiceBusPublisher serviceBusPublisher)
        {
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task SynchronizeChallengesAsync(Game game)
        {
            await _serviceBusPublisher.PublishChallengesSynchronizedIntegrationEventAsync(game);
        }
    }
}
