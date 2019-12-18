// Filename: ChallengeSynchronizedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
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
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;

        public ChallengeSynchronizedIntegrationEventHandler(
            IChallengeService challengeService,
            IAccountService accountService,
            ILogger<ChallengeSynchronizedIntegrationEventHandler> logger
        )
        {
            _challengeService = challengeService;
            _accountService = accountService;
            _logger = logger;
        }

        public async Task HandleAsync(ChallengeSynchronizedIntegrationEvent integrationEvent)
        {
            var challengeId = integrationEvent.ChallengeId.ParseEntityId<ChallengeId>();

            if (await _challengeService.ChallengeExistsAsync(challengeId))
            {
                var challenge = await _challengeService.FindChallengeAsync(challengeId);

                var scoreboard = new Scoreboard(
                    challenge.Payout,
                    integrationEvent.Scoreboard.ToDictionary(position => position.Key.ParseEntityId<UserId>(), position => position.Value?.ToDecimal()));

                var result = await _accountService.PayoutChallengeAsync(scoreboard);

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
                _logger.LogCritical(""); // FRANCIS: TODO.
            }
        }
    }
}
