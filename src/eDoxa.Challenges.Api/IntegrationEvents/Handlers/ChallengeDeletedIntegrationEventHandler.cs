// Filename: ChallengeCreationFailedIntegrationEventHandler.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.IntegrationEvents.Handlers
{
    public sealed class ChallengeDeletedIntegrationEventHandler : IIntegrationEventHandler<ChallengeDeletedIntegrationEvent>
    {
        private readonly IChallengeService _challengeService;

        public ChallengeDeletedIntegrationEventHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        public async Task HandleAsync(ChallengeDeletedIntegrationEvent integrationEvent)
        {
            var challenge = await _challengeService.FindChallengeAsync(integrationEvent.ChallengeId);

            if (challenge != null)
            {
                await _challengeService.DeleteChallengeAsync(challenge);
            }
        }
    }
}
