// Filename: ChallengeClosedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Services;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Api.IntegrationEvents.Handlers
{
    public sealed class ChallengeClosedIntegrationEventHandler : IIntegrationEventHandler<ChallengeClosedIntegrationEvent>
    {
        private readonly IChallengeService _challengeService;
        private readonly ILogger _logger;

        public ChallengeClosedIntegrationEventHandler(
            IChallengeService challengeService,
            ILogger<ChallengeClosedIntegrationEventHandler> logger
        )
        {
            _challengeService = challengeService;
            _logger = logger;
        }

        public async Task HandleAsync(ChallengeClosedIntegrationEvent integrationEvent)
        {
            var challengeId = integrationEvent.ChallengeId.ParseEntityId<ChallengeId>();

            if (await _challengeService.ChallengeExistsAsync(challengeId))
            {
                var challenge = await _challengeService.FindChallengeAsync(challengeId);
                    
                var result = await _challengeService.CloseChallengeAsync(challenge, new UtcNowDateTimeProvider());

                if (result.IsValid)
                {
                    _logger.LogInformation(""); // FRANCIS: TODO.
                }
                else
                {
                    _logger.LogCritical(""); // FRANCIS: TODO.
                }
            }
            else
            {
                _logger.LogWarning(""); // FRANCIS: TODO.
            }
        }
    }
}
