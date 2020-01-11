// Filename: ChallengeParticipantsController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Web.Aggregator.Mappers;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Challenges.Requests;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Requests;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using static eDoxa.Grpc.Protos.Cashier.Requests.CreateTransactionRequest.Types;
using static eDoxa.Grpc.Protos.Challenges.Aggregates.ChallengeAggregate.Types;

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
        private readonly GameService.GameServiceClient _gameServiceClient;

        public ChallengeParticipantsController(
            ChallengeService.ChallengeServiceClient challengesServiceClient,
            CashierService.CashierServiceClient cashierServiceClient,
            IdentityService.IdentityServiceClient identityServiceClient,
            GameService.GameServiceClient gameServiceClient
        )
        {
            _challengesServiceClient = challengesServiceClient;
            _cashierServiceClient = cashierServiceClient;
            _identityServiceClient = identityServiceClient;
            _gameServiceClient = gameServiceClient;
        }

        [HttpPost]
        [SwaggerOperation("Register a participant to a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ParticipantAggregate))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> RegisterChallengeParticipantAsync(string challengeId)
        {
            var participantId = Guid.NewGuid().ToString();

            var findChallengeRequest = new FindChallengeRequest
            {
                ChallengeId = challengeId
            };

            var findChallengeResponse = await _challengesServiceClient.FindChallengeAsync(findChallengeRequest);

            var findPlayerGameCredentialRequest = new FindPlayerGameCredentialRequest
            {
                Game = findChallengeResponse.Challenge.Game
            };

            var findPlayerGameCredentialResponse = await _gameServiceClient.FindPlayerGameCredentialAsync(findPlayerGameCredentialRequest);

            var fetchDoxatagsRequest = new FetchDoxatagsRequest();

            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(fetchDoxatagsRequest);

            var findChallengePayoutRequest = new FindChallengePayoutRequest
            {
                ChallengeId = challengeId
            };

            var challengePayoutResponse = await _cashierServiceClient.FindChallengePayoutAsync(findChallengePayoutRequest);

            var createTransactionRequest = new CreateTransactionRequest
            {
                Custom = new CustomTransaction
                {
                    Type = EnumTransactionType.Charge,
                    Amount = challengePayoutResponse.Payout.EntryFee.Amount,
                    Currency = challengePayoutResponse.Payout.EntryFee.Currency
                },
                Metadata =
                {
                    new Dictionary<string, string>
                    {
                        [nameof(ChallengeId)] = challengeId,
                        [nameof(ParticipantId)] = participantId
                    }
                }
            };

            await _cashierServiceClient.CreateTransactionAsync(createTransactionRequest);

            var registerChallengeParticipantRequest = new RegisterChallengeParticipantRequest
            {
                ChallengeId = challengeId,
                GamePlayerId = findPlayerGameCredentialResponse.Credential.PlayerId,
                ParticipantId = participantId
            };

            var participant = await _challengesServiceClient.RegisterChallengeParticipantAsync(registerChallengeParticipantRequest);

            return this.Ok(ChallengeMapper.Map(challengePayoutResponse.Payout.ChallengeId, participant.Participant, fetchDoxatagsResponse.Doxatags));
        }
    }
}
