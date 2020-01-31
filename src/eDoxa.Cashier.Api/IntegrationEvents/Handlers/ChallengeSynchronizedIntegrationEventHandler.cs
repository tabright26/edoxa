// Filename: ChallengeSynchronizedIntegrationEventHandler.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class ChallengeSynchronizedIntegrationEventHandler : IIntegrationEventHandler<ChallengeSynchronizedIntegrationEvent>
    {
        private readonly IChallengeService _challengeService;
        private readonly ILogger _logger;

        public ChallengeSynchronizedIntegrationEventHandler(IChallengeService challengeService, ILogger<ChallengeSynchronizedIntegrationEventHandler> logger)
        {
            _challengeService = challengeService;
            _logger = logger;
        }

        public async Task HandleAsync(ChallengeSynchronizedIntegrationEvent integrationEvent)
        {
            var challengeId = integrationEvent.ChallengeId.ParseEntityId<ChallengeId>();

            if (await _challengeService.ChallengeExistsAsync(challengeId))
            {
                var challenge = await _challengeService.FindChallengeAsync(challengeId);

                await _challengeService.CloseChallengeAsync(
                    challenge,
                    integrationEvent.Scoreboard.ToDictionary(
                        participant => participant.UserId.ParseEntityId<UserId>(),
                        participant => participant.Score?.ToDecimal()));

                _logger.LogInformation(""); // FRANCIS: TODO.
            }
            else
            {
                _logger.LogCritical(""); // FRANCIS: TODO.
            }
        }
    }
}
