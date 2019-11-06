// Filename: ChallengesSynchronizedIntegrationEventHandler.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
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
            await _challengeService.SynchronizeChallengesAsync(Game.LeagueOfLegends, new UtcNowDateTimeProvider());
        }
    }
}
