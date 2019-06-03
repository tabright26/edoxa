// Filename: MatchesController.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/matches")]
    [ApiExplorerSettings(GroupName = "Matches")]
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
        [HttpGet("{matchId}", Name = nameof(FindMatchAsync))]
        public async Task<IActionResult> FindMatchAsync(MatchId matchId)
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