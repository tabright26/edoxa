// Filename: ChallengesController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Web.Aggregator.IntegrationEvents.Extensions;
using eDoxa.Challenges.Web.Aggregator.Models;
using eDoxa.Challenges.Web.Aggregator.Requests;
using eDoxa.Challenges.Web.Aggregator.Transformers;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Requests;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;
using eDoxa.ServiceBus.Abstractions;

using Grpc.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using CashierRequests = eDoxa.Grpc.Protos.Cashier.Requests;
using ChallengeRequests = eDoxa.Grpc.Protos.Challenges.Requests;

namespace eDoxa.Challenges.Web.Aggregator.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengesController : ControllerBase
    {
        private readonly IdentityService.IdentityServiceClient _identityServiceClient;
        private readonly ChallengeService.ChallengeServiceClient _challengesServiceClient;
        private readonly CashierService.CashierServiceClient _cashierServiceClient;
        private readonly GameService.GameServiceClient _gameServiceClient;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ChallengesController(
            IdentityService.IdentityServiceClient identityServiceClient,
            ChallengeService.ChallengeServiceClient challengesServiceClient,
            CashierService.CashierServiceClient cashierServiceClient,
            GameService.GameServiceClient gameServiceClient,
            IServiceBusPublisher serviceBusPublisher
        )
        {
            _identityServiceClient = identityServiceClient;
            _challengesServiceClient = challengesServiceClient;
            _cashierServiceClient = cashierServiceClient;
            _gameServiceClient = gameServiceClient;
            _serviceBusPublisher = serviceBusPublisher;
        }

        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation("Fetch challenges.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeModel[]))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> FetchChallengesAsync()
        {
            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(new FetchDoxatagsRequest());

            var fetchChallengePayoutsResponse = await _cashierServiceClient.FetchChallengePayoutsAsync(new CashierRequests.FetchChallengePayoutsRequest());

            var fetchChallengesResponse = await _challengesServiceClient.FetchChallengesAsync(new ChallengeRequests.FetchChallengesRequest());

            return this.Ok(
                ChallengeTransformer.Transform(fetchChallengesResponse.Challenges, fetchChallengePayoutsResponse.Payouts, fetchDoxatagsResponse.Doxatags));
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost]
        [SwaggerOperation("Create challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> CreateChallengeAsync([FromBody] CreateChallengeRequest request)
        {
            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(new FetchDoxatagsRequest());

            var fetchChallengeScoringResponse = await _gameServiceClient.FetchChallengeScoringAsync(new FetchChallengeScoringRequest());

            var createChallengeResponse = await _challengesServiceClient.CreateChallengeAsync(
                new ChallengeRequests.CreateChallengeRequest
                {
                    Name = request.Name,
                    Game = request.Game,
                    BestOf = request.BestOf,
                    Entries = request.Entries,
                    Duration = request.Duration,
                    Scoring =
                    {
                        fetchChallengeScoringResponse.Scoring
                    }
                });

            var challenge = createChallengeResponse.Challenge;

            try
            {
                var createChallengePayoutResponse = await _cashierServiceClient.CreateChallengePayoutAsync(
                    new CashierRequests.CreateChallengePayoutRequest
                    {
                        ChallengeId = challenge.Id,
                        PayoutEntries = challenge.Entries / 2,
                        EntryFee = new EntryFeeDto
                        {
                            Amount = request.EntryFeeAmount,
                            Currency = request.EntryFeeCurrency
                        }
                    });

                return this.Ok(ChallengeTransformer.Transform(challenge, createChallengePayoutResponse.Payout, fetchDoxatagsResponse.Doxatags));
            }
            catch (RpcException exception)
            {
                await _serviceBusPublisher.PublishChallengeDeletedIntegrationEventAsync(ChallengeId.Parse(challenge.Id));

                return this.BadRequest(exception);
            }
        }

        [AllowAnonymous]
        [HttpGet("{challengeId}")]
        [SwaggerOperation("Find a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> FindChallengeAsync(string challengeId)
        {
            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(new FetchDoxatagsRequest());

            var findChallengePayoutResponse = await _cashierServiceClient.FindChallengePayoutAsync(
                new CashierRequests.FindChallengePayoutRequest
                {
                    ChallengeId = challengeId
                });

            var findChallengeResponse = await _challengesServiceClient.FindChallengeAsync(
                new ChallengeRequests.FindChallengeRequest
                {
                    ChallengeId = challengeId
                });

            return this.Ok(ChallengeTransformer.Transform(findChallengeResponse.Challenge, findChallengePayoutResponse.Payout, fetchDoxatagsResponse.Doxatags));
        }

        //[Microsoft.AspNetCore.Authorization.Authorize(Roles = AppRoles.Admin)]
        //[HttpPost("{challengeId}")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeModel))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        //public async Task<IActionResult> SynchronizeChallengeAsync(Guid challengeId)
        //{
        //    var doxatagsFromIdentityService = await _identityService.FetchDoxatagsAsync();

        //    var challengeFromCashierService = await _cashierService.FindChallengeAsync(challengeId);

        //    var challengeFromChallengesService = await _challengesService.SynchronizeChallengeAsync(challengeId);

        //    return this.Ok(ChallengeTransformer.Transform(challengeFromChallengesService, challengeFromCashierService, doxatagsFromIdentityService));
        //}
    }
}
