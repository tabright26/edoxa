// Filename: ChallengeParticipantsController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Web.Aggregator.Transformers;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using static eDoxa.Grpc.Protos.Challenges.Aggregates.ChallengeAggregate.Types;

using CashierRequests = eDoxa.Grpc.Protos.Cashier.Requests;
using ChallengeRequests = eDoxa.Grpc.Protos.Challenges.Requests;

namespace eDoxa.Challenges.Web.Aggregator.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/challenges/{challengeId}/participants")]
    [ApiExplorerSettings(GroupName = "Challenges")]
    public sealed class ChallengeParticipantsController : ControllerBase
    {
        private readonly ChallengeService.ChallengeServiceClient _challengesServiceClient;
        private readonly CashierService.CashierServiceClient _cashierServiceClient;
        private readonly IdentityService.IdentityServiceClient _identityServiceClient;

        public ChallengeParticipantsController(
            ChallengeService.ChallengeServiceClient challengesServiceClient,
            CashierService.CashierServiceClient cashierServiceClient,
            IdentityService.IdentityServiceClient identityServiceClient
        )
        {
            _challengesServiceClient = challengesServiceClient;
            _cashierServiceClient = cashierServiceClient;
            _identityServiceClient = identityServiceClient;
        }

        [HttpPost]
        [SwaggerOperation("Register a participant to a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ParticipantAggregate))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> RegisterChallengeParticipantAsync(string challengeId)
        {
            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(new FetchDoxatagsRequest());

            var findChallengeResponse = await _cashierServiceClient.FindChallengePayoutAsync(
                new CashierRequests.FindChallengePayoutRequest
                {
                    ChallengeId = challengeId
                });

            var participantId = Guid.NewGuid().ToString();

            var metadata = new Dictionary<string, string>
            {
                [nameof(ChallengeId)] = challengeId,
                [nameof(ParticipantId)] = participantId
            };

            await _cashierServiceClient.CreateTransactionAsync(
                new CashierRequests.CreateTransactionRequest
                {
                    Type = TransactionTypeDto.Charge,
                    Amount = findChallengeResponse.Payout.EntryFee.Amount,
                    Currency = findChallengeResponse.Payout.EntryFee.Currency,
                    Metadata =
                    {
                        metadata
                    }
                });

            var participant = await _challengesServiceClient.RegisterChallengeParticipantAsync(
                new ChallengeRequests.RegisterChallengeParticipantRequest
                {
                    ChallengeId = challengeId,
                    GamePlayerId = Guid.NewGuid().ToString(),
                    ParticipantId = participantId
                });

            return this.Ok(ChallengeMapper.Map(findChallengeResponse.Payout.ChallengeId, participant.Participant, fetchDoxatagsResponse.Doxatags));
        }
    }
}
