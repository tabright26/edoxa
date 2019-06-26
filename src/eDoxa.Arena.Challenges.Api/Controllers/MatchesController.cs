// Filename: MatchesController.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Domain.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/matches")]
    [ApiExplorerSettings(GroupName = "Match")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchQuery _matchQuery;

        public MatchesController(IMatchQuery matchQuery)
        {
            _matchQuery = matchQuery;
        }

        /// <summary>
        ///     Find a match.
        /// </summary>
        [HttpGet("{matchId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(MatchViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(MatchId matchId)
        {
            var match = await _matchQuery.FindMatchAsync(matchId);

            if (match == null)
            {
                return this.NotFound("Match not found.");
            }

            return this.Ok(match);
        }
    }
}
