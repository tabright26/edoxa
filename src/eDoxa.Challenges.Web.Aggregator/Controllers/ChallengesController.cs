// Filename: ChallengesController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Web.Aggregator.Mappers;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Challenges.Aggregates;
using eDoxa.Grpc.Protos.Challenges.Enums;
using eDoxa.Grpc.Protos.Challenges.Requests;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Grpc.Protos.Games.Requests;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Extensions;

using Grpc.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Challenges.Web.Aggregator.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenges")]
    public sealed class ChallengesController : ControllerBase
    {
        private readonly IdentityService.IdentityServiceClient _identityServiceClient;
        private readonly ChallengeService.ChallengeServiceClient _challengesServiceClient;
        private readonly CashierService.CashierServiceClient _cashierServiceClient;
        private readonly GameService.GameServiceClient _gameServiceClient;

        public ChallengesController(
            IdentityService.IdentityServiceClient identityServiceClient,
            ChallengeService.ChallengeServiceClient challengesServiceClient,
            CashierService.CashierServiceClient cashierServiceClient,
            GameService.GameServiceClient gameServiceClient
        )
        {
            _identityServiceClient = identityServiceClient;
            _challengesServiceClient = challengesServiceClient;
            _cashierServiceClient = cashierServiceClient;
            _gameServiceClient = gameServiceClient;
        }

        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation("Fetch challenges.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeAggregate[]))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> FetchChallengesAsync(
            EnumGame game = EnumGame.None,
            EnumChallengeState state = EnumChallengeState.None,
            bool includeMatches = false
        )
        {
            var fetchDoxatagsRequest = new FetchDoxatagsRequest();

            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(fetchDoxatagsRequest);

            var fetchChallengePayoutsRequest = new FetchChallengePayoutsRequest();

            var fetchChallengePayoutsResponse = await _cashierServiceClient.FetchChallengePayoutsAsync(fetchChallengePayoutsRequest);

            var fetchChallengesRequest = new FetchChallengesRequest
            {
                Game = game,
                State = state,
                IncludeMatches = includeMatches
            };

            var fetchChallengesResponse = await _challengesServiceClient.FetchChallengesAsync(fetchChallengesRequest);

            return this.Ok(ChallengeMapper.Map(fetchChallengesResponse.Challenges, fetchChallengePayoutsResponse.Payouts, fetchDoxatagsResponse.Doxatags));
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost]
        [SwaggerOperation("Create challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeAggregate))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> CreateChallengeAsync([FromBody] Requests.CreateChallengeRequest request)
        {
            var fetchDoxatagsRequest = new FetchDoxatagsRequest();

            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(fetchDoxatagsRequest);

            var findChallengeScoringRequest = new FindChallengeScoringRequest
            {
                Game = request.Game
            };

            var findChallengeScoringResponse = await _gameServiceClient.FindChallengeScoringAsync(findChallengeScoringRequest);

            var createChallengeRequest = new CreateChallengeRequest
            {
                Name = request.Name,
                Game = request.Game,
                BestOf = request.BestOf,
                Entries = request.Entries,
                Duration = request.Duration,
                Scoring = findChallengeScoringResponse.Scoring
            };

            var createChallengeResponse = await _challengesServiceClient.CreateChallengeAsync(createChallengeRequest);

            try
            {
                var createChallengePayoutRequest = new CreateChallengePayoutRequest
                {
                    ChallengeId = createChallengeResponse.Challenge.Id,
                    PayoutEntries = createChallengeResponse.Challenge.Entries / 2, // TODO
                    EntryFee = new CurrencyDto
                    {
                        Amount = request.EntryFee.Amount,
                        Type = request.EntryFee.Type
                    }
                };

                var createChallengePayoutResponse = await _cashierServiceClient.CreateChallengePayoutAsync(createChallengePayoutRequest);

                return this.Ok(ChallengeMapper.Map(createChallengeResponse.Challenge, createChallengePayoutResponse.Payout, fetchDoxatagsResponse.Doxatags));
            }
            catch (RpcException exception)
            {
                var deleteChallengeRequest = new DeleteChallengeRequest
                {
                    ChallengeId = createChallengeResponse.Challenge.Id
                };

                await _challengesServiceClient.DeleteChallengeAsync(deleteChallengeRequest);

                throw exception.Capture();
            }
        }

        [AllowAnonymous]
        [HttpGet("{challengeId}")]
        [SwaggerOperation("Find a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeAggregate))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> FindChallengeAsync(string challengeId)
        {
            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(new FetchDoxatagsRequest());

            var findChallengePayoutRequest = new FindChallengePayoutRequest
            {
                ChallengeId = challengeId
            };

            var findChallengePayoutResponse = await _cashierServiceClient.FindChallengePayoutAsync(findChallengePayoutRequest);

            var findChallengeRequest = new FindChallengeRequest
            {
                ChallengeId = challengeId
            };

            var findChallengeResponse = await _challengesServiceClient.FindChallengeAsync(findChallengeRequest);

            return this.Ok(ChallengeMapper.Map(findChallengeResponse.Challenge, findChallengePayoutResponse.Payout, fetchDoxatagsResponse.Doxatags));
        }
    }
}
