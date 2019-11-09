// Filename: ChallengeParticipantsController.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Challenges.Aggregator.IntegrationEvents.Extensions;
using eDoxa.Challenges.Aggregator.Services;
using eDoxa.Challenges.Aggregator.Transformers;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Refit;

using Swashbuckle.AspNetCore.Annotations;

using CreateTransactionRequestFromCashierService = eDoxa.Cashier.Requests.CreateTransactionRequest;
using RegisterChallengeParticipantRequestFromChallengesService = eDoxa.Challenges.Requests.RegisterChallengeParticipantRequest;

namespace eDoxa.Challenges.Aggregator.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [ApiController]
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

        /// <summary>
        ///     Register a participant to a challenge.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> RegisterChallengeParticipantAsync(ChallengeId challengeId)
        {
            var doxatags = await _identityService.FetchDoxatagsAsync();

            var challenge = await _cashierService.FindChallengeAsync(challengeId);

            var participantId = new ParticipantId();

            await _cashierService.CreateTransactionAsync(
                new CreateTransactionRequestFromCashierService(
                    TransactionType.Charge.Name,
                    challenge.EntryFee.Currency,
                    challenge.EntryFee.Amount,
                    new Dictionary<string, string>
                    {
                        [nameof(ChallengeId)] = challengeId.ToString(),
                        [nameof(ParticipantId)] = participantId.ToString()
                    }));

            try
            {
                var participant = await _challengesService.RegisterChallengeParticipantAsync(
                    challengeId,
                    new RegisterChallengeParticipantRequestFromChallengesService(participantId));

                return this.Ok(ChallengeTransformer.Transform(participant, doxatags));
            }
            catch (ApiException exception)
            {
                await _serviceBusPublisher.PublishParticipantRegistrationFailedIntegrationEventAsync(challengeId, participantId);

                return this.BadRequest(exception.Content);
            }
        }
    }
}
