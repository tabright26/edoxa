// Filename: ChallengeHistoryController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Infrastructure.Queries.Extensions;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Responses;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Challenges.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/challenges/history")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengeHistoryController : ControllerBase
    {
        private readonly IChallengeQuery _challengeQuery;

        public ChallengeHistoryController(IChallengeQuery challengeQuery)
        {
            _challengeQuery = challengeQuery;
        }

        [HttpGet]
        [SwaggerOperation("Find the challenge history of a user.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeResponse[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync(Game? game = null, ChallengeState? state = null)
        {
            var responses = await _challengeQuery.FetchUserChallengeHistoryResponsesAsync(game, state);

            if (!responses.Any())
            {
                return this.NoContent();
            }

            return this.Ok(responses);
        }
    }
}
