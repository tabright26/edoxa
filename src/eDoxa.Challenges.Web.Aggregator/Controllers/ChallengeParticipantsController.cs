// Filename: ChallengeParticipantsController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Web.Aggregator.IntegrationEvents.Extensions;
using eDoxa.Challenges.Web.Aggregator.Models;
using eDoxa.Challenges.Web.Aggregator.Transformers;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Grpc.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using CashierRequests = eDoxa.Grpc.Protos.Cashier.Requests;
using ChallengeRequests = eDoxa.Grpc.Protos.Challenges.Requests;
using TransactionType = eDoxa.Grpc.Protos.Cashier.Enums.TransactionType;

namespace eDoxa.Challenges.Web.Aggregator.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/challenges/{challengeId}/participants")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengeParticipantsController : ControllerBase
    {
        private readonly ChallengeService.ChallengeServiceClient _challengesServiceClient;
        private readonly CashierService.CashierServiceClient _cashierServiceClient;
        private readonly IdentityService.IdentityServiceClient _identityServiceClient;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ChallengeParticipantsController(
            ChallengeService.ChallengeServiceClient challengesServiceClient,
            CashierService.CashierServiceClient cashierServiceClient,
            IdentityService.IdentityServiceClient identityServiceClient,
            IServiceBusPublisher serviceBusPublisher
        )
        {
            _challengesServiceClient = challengesServiceClient;
            _cashierServiceClient = cashierServiceClient;
            _identityServiceClient = identityServiceClient;
            _serviceBusPublisher = serviceBusPublisher;
        }

        [HttpPost]
        [SwaggerOperation("Register a participant to a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ParticipantModel))]
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

            var createTransactionResponse = await _cashierServiceClient.CreateTransactionAsync(
                new CashierRequests.CreateTransactionRequest
                {
                    Type = TransactionType.Charge,
                    Amount = findChallengeResponse.Payout.EntryFee.Amount,
                    Currency = findChallengeResponse.Payout.EntryFee.Currency,
                    Metadata =
                    {
                        metadata
                    }
                });

            try
            {
                var participant = await _challengesServiceClient.RegisterChallengeParticipantAsync(
                    new ChallengeRequests.RegisterChallengeParticipantRequest
                    {
                        ChallengeId = challengeId,
                        GamePlayerId = Guid.NewGuid().ToString(),
                        ParticipantId = participantId
                    });

                return this.Ok(ChallengeTransformer.Transform(findChallengeResponse.Payout.ChallengeId, participant.Participant, fetchDoxatagsResponse.Doxatags));
            }
            catch (RpcException exception)
            {
                await _serviceBusPublisher.PublishTransactionCanceledIntegrationEventAsync(TransactionId.Parse(createTransactionResponse.Transaction.Id));

                return this.BadRequest();
            }
        }
    }
}
