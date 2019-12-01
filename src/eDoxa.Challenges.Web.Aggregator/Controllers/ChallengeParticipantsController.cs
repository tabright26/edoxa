// Filename: ChallengeParticipantsController.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Web.Aggregator.IntegrationEvents.Extensions;
using eDoxa.Challenges.Web.Aggregator.Models;
using eDoxa.Challenges.Web.Aggregator.Services;
using eDoxa.Challenges.Web.Aggregator.Transformers;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Refit;

using Swashbuckle.AspNetCore.Annotations;

using CashierRequests = eDoxa.Cashier.Requests;
using ChallengeRequests = eDoxa.Challenges.Requests;

namespace eDoxa.Challenges.Web.Aggregator.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [ApiVersion("1.0")]
    [Route("api/challenges/{challengeId}/participants")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengeParticipantsController : ControllerBase
    {
        private readonly IChallengesService _challengesService;
        private readonly ICashierService _cashierService;
        private readonly IIdentityService _identityService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ChallengeParticipantsController(
            IChallengesService challengesService,
            ICashierService cashierService,
            IIdentityService identityService,
            IServiceBusPublisher serviceBusPublisher
        )
        {
            _challengesService = challengesService;
            _cashierService = cashierService;
            _identityService = identityService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        [HttpPost]
        [SwaggerOperation("Register a participant to a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ParticipantModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> RegisterChallengeParticipantAsync(ChallengeId challengeId)
        {
            var doxatags = await _identityService.FetchDoxatagsAsync();

            var challenge = await _cashierService.FindChallengeAsync(challengeId);

            var participantId = new ParticipantId();

            var metadata = new Dictionary<string, string>
            {
                [nameof(ChallengeId)] = challengeId.ToString(),
                [nameof(ParticipantId)] = participantId.ToString()
            };

            var transactionId = new TransactionId();

            await _cashierService.CreateTransactionAsync(
                new CashierRequests.CreateTransactionRequest(
                    transactionId,
                    TransactionType.Charge.Name,
                    challenge.EntryFee.Currency,
                    challenge.EntryFee.Amount,
                    metadata));

            try
            {
                var participant = await _challengesService.RegisterChallengeParticipantAsync(
                    challengeId,
                    new ChallengeRequests.RegisterChallengeParticipantRequest(participantId));

                return this.Ok(ChallengeTransformer.Transform(challenge.Id, participant, doxatags));
            }
            catch (ApiException exception)
            {
                await _serviceBusPublisher.PublishTransactionCanceledIntegrationEventAsync(transactionId);

                return this.BadRequest(JsonConvert.DeserializeObject<ValidationProblemDetails>(exception.Content));
            }
        }
    }
}
