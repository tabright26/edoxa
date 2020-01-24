// Filename: ChallengeHistoryController.cs
// Date Created: 2020-01-24
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Web.Aggregator.Mappers;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Challenges.Aggregates;
using eDoxa.Grpc.Protos.Challenges.Enums;
using eDoxa.Grpc.Protos.Challenges.Requests;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Challenges.Web.Aggregator.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/challenge-history")]
    [ApiExplorerSettings(GroupName = "Challenges")]
    public sealed class ChallengeHistoryController : ControllerBase
    {
        private readonly IdentityService.IdentityServiceClient _identityServiceClient;
        private readonly ChallengeService.ChallengeServiceClient _challengesServiceClient;
        private readonly CashierService.CashierServiceClient _cashierServiceClient;

        public ChallengeHistoryController(
            IdentityService.IdentityServiceClient identityServiceClient,
            ChallengeService.ChallengeServiceClient challengesServiceClient,
            CashierService.CashierServiceClient cashierServiceClient
        )
        {
            _identityServiceClient = identityServiceClient;
            _challengesServiceClient = challengesServiceClient;
            _cashierServiceClient = cashierServiceClient;
        }

        [HttpGet]
        [SwaggerOperation("Fetch challenges.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeAggregate[]))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> FetchChallengesAsync(EnumGame game = EnumGame.None, EnumChallengeState state = EnumChallengeState.None)
        {
            var fetchDoxatagsResponse = await _identityServiceClient.FetchDoxatagsAsync(new FetchDoxatagsRequest());

            var fetchChallengePayoutsResponse = await _cashierServiceClient.FetchChallengePayoutsAsync(new FetchChallengePayoutsRequest());

            var fetchChallengesResponse = await _challengesServiceClient.FetchChallengeHistoryAsync(
                new FetchChallengeHistoryRequest
                {
                    Game = game,
                    State = state
                });

            return this.Ok(ChallengeMapper.Map(fetchChallengesResponse.Challenges, fetchChallengePayoutsResponse.Payouts, fetchDoxatagsResponse.Doxatags));
        }
    }
}
