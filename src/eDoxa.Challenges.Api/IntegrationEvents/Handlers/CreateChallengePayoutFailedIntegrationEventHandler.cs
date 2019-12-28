﻿// Filename: CreateChallengePayoutFailedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Services;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Api.IntegrationEvents.Handlers
{
    public sealed class CreateChallengePayoutFailedIntegrationEventHandler : IIntegrationEventHandler<CreateChallengePayoutFailedIntegrationEvent>
    {
        private readonly IChallengeService _challengeService;
        private readonly ILogger _logger;

        public CreateChallengePayoutFailedIntegrationEventHandler(
            IChallengeService challengeService,
            ILogger<CreateChallengePayoutFailedIntegrationEventHandler> logger
        )
        {
            _challengeService = challengeService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateChallengePayoutFailedIntegrationEvent integrationEvent)
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