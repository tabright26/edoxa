// Filename: ChallengesSynchronizedIntegrationEventHandler.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Services;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.IntegrationEvents.Handlers
{
    public sealed class ChallengesSynchronizedIntegrationEventHandler : IIntegrationEventHandler<ChallengesSynchronizedIntegrationEvent>
    {
        private readonly IChallengeService _challengeService;

        public ChallengesSynchronizedIntegrationEventHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        public async Task HandleAsync(ChallengesSynchronizedIntegrationEvent integrationEvent)
        {
            await _challengeService.SynchronizeChallengesAsync(Game.FromValue((int) integrationEvent.Game), new UtcNowDateTimeProvider());
        }
    }
}
