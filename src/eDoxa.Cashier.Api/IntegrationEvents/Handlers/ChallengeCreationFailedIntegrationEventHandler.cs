// Filename: ChallengeCreationFailedIntegrationEventHandler.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
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
