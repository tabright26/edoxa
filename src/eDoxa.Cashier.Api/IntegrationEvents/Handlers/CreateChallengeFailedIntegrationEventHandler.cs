// Filename: CreateChallengeFailedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class CreateChallengeFailedIntegrationEventHandler : IIntegrationEventHandler<CreateChallengeFailedIntegrationEvent>
    {
        private readonly IChallengeService _challengeService;
        private readonly ILogger _logger;

        public CreateChallengeFailedIntegrationEventHandler(IChallengeService challengeService, ILogger<CreateChallengeFailedIntegrationEventHandler> logger)
        {
            _challengeService = challengeService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateChallengeFailedIntegrationEvent integrationEvent)
        {
            var challengeId = integrationEvent.ChallengeId.ParseEntityId<ChallengeId>();

            if (await _challengeService.ChallengeExistsAsync(challengeId))
            {
                var challenge = await _challengeService.FindChallengeAsync(challengeId);

                var result = await _challengeService.DeleteChallengeAsync(challenge);

                if (result.IsValid)
                {
                    _logger.LogInformation(""); // FRANCIS: TODO.
                }
                else
                {
                    _logger.LogError(""); // FRANCIS: TODO.  
                }
            }
            else
            {
                _logger.LogWarning(""); // FRANCIS: TODO.
            }
        }
    }
}
