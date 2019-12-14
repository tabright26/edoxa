// Filename: ChallengesController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Web.Aggregator.Transformers;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Challenges.Aggregates;
using eDoxa.Grpc.Protos.Challenges.Requests;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Requests;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Seedwork.Security;

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
        public async Task<IActionResult> FetchChallengesAsync()
        {
            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(new FetchDoxatagsRequest());

            var fetchChallengePayoutsResponse = await _cashierServiceClient.FetchChallengePayoutsAsync(new FetchChallengePayoutsRequest());

            var fetchChallengesResponse = await _challengesServiceClient.FetchChallengesAsync(new FetchChallengesRequest());

            return this.Ok(ChallengeMapper.Map(fetchChallengesResponse.Challenges, fetchChallengePayoutsResponse.Payouts, fetchDoxatagsResponse.Doxatags));
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost]
        [SwaggerOperation("Create challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeAggregate))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> CreateChallengeAsync([FromBody] CreateChallangeAggregateRequest request)
        {
            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(new FetchDoxatagsRequest());

            var fetchChallengeScoringResponse = await _gameServiceClient.FetchChallengeScoringAsync(
                new FetchChallengeScoringRequest
                {
                    Game = request.Game
                });

            var createChallengeResponse = await _challengesServiceClient.CreateChallengeAsync(
                new CreateChallengeRequest
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

            var createChallengePayoutResponse = await _cashierServiceClient.CreateChallengePayoutAsync(
                new CreateChallengePayoutRequest
                {
                    ChallengeId = challenge.Id,
                    PayoutEntries = challenge.Entries / 2, // TODO
                    EntryFee = new EntryFeeDto
                    {
                        Amount = request.EntryFee.Amount,
                        Currency = request.EntryFee.Currency
                    }
                });

            return this.Ok(ChallengeMapper.Map(challenge, createChallengePayoutResponse.Payout, fetchDoxatagsResponse.Doxatags));
        }

        [AllowAnonymous]
        [HttpGet("{challengeId}")]
        [SwaggerOperation("Find a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeAggregate))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> FindChallengeAsync(string challengeId)
        {
            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(new FetchDoxatagsRequest());

            var findChallengePayoutResponse = await _cashierServiceClient.FindChallengePayoutAsync(
                new FindChallengePayoutRequest
                {
                    ChallengeId = challengeId
                });

            var findChallengeResponse = await _challengesServiceClient.FindChallengeAsync(
                new FindChallengeRequest
                {
                    ChallengeId = challengeId
                });

            return this.Ok(ChallengeMapper.Map(findChallengeResponse.Challenge, findChallengePayoutResponse.Payout, fetchDoxatagsResponse.Doxatags));
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
