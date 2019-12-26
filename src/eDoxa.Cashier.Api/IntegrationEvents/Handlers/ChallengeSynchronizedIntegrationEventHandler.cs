// Filename: ChallengeSynchronizedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
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
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly ILogger _logger;

        public ChallengeSynchronizedIntegrationEventHandler(
            IChallengeService challengeService,
            IAccountService accountService,
            IServiceBusPublisher serviceBusPublisher,
            ILogger<ChallengeSynchronizedIntegrationEventHandler> logger
        )
        {
            _challengeService = challengeService;
            _accountService = accountService;
            _serviceBusPublisher = serviceBusPublisher;
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
                    integrationEvent.Scoreboard.ToDictionary(participant => participant.UserId.ParseEntityId<UserId>(), participant => participant.Score?.ToDecimal()));

                var result = await _accountService.PayoutChallengeAsync(scoreboard);

                if (result.IsValid)
                {
                    await _serviceBusPublisher.PublishChallengeClosedIntegrationEventAsync(challengeId, result.GetEntityFromMetadata<PayoutPrizes>());

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
