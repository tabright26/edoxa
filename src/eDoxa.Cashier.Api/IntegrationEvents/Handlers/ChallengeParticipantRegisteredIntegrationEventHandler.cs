﻿// Filename: ChallengeParticipantRegisteredIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.IntegrationEvents.Handlers
{
    public sealed class ChallengeParticipantRegisteredIntegrationEventHandler : IIntegrationEventHandler<ChallengeParticipantRegisteredIntegrationEvent>
    {
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;

        public ChallengeParticipantRegisteredIntegrationEventHandler(
            IAccountService accountService,
            ILogger<ChallengeParticipantRegisteredIntegrationEventHandler> logger
        )
        {
            _accountService = accountService;
            _logger = logger;
        }

        public async Task HandleAsync(ChallengeParticipantRegisteredIntegrationEvent integrationEvent)
        {
            var participant = integrationEvent.Participant;

            var userId = participant.UserId.ParseEntityId<UserId>();

            var participantId = participant.Id.ParseEntityId<ParticipantId>();

            var challengeId = participant.ChallengeId.ParseEntityId<ChallengeId>();

            if (await _accountService.AccountExistsAsync(userId))
            {
                var account = await _accountService.FindAccountAsync(userId);

                var metadata = new TransactionMetadata
                {
                    [nameof(ChallengeId)] = challengeId,
                    [nameof(ParticipantId)] = participantId
                };

                var result = await _accountService.MarkAccountTransactionAsSucceededAsync(account, metadata);

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
                _logger.LogCritical(""); // FRANCIS: TODO.
            }
        }
    }
}
