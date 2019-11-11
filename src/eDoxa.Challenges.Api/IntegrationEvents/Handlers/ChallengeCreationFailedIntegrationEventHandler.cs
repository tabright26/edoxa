// Filename: ChallengeCreationFailedIntegrationEventHandler.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.IntegrationEvents.Handlers
{
    public sealed class ChallengeCreationFailedIntegrationEventHandler : IIntegrationEventHandler<ChallengeCreationFailedIntegrationEvent>
    {
        private readonly IChallengeService _challengeService;

        public ChallengeCreationFailedIntegrationEventHandler(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        public async Task HandleAsync(ChallengeCreationFailedIntegrationEvent integrationEvent)
        {
            var challenge = await _challengeService.FindChallengeAsync(integrationEvent.ChallengeId);

            if (challenge != null)
            {
                await _challengeService.DeleteChallengeAsync(challenge);
            }
        }
    }
}
