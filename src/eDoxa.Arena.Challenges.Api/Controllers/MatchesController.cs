// Filename: MatchesController.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
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
        private readonly IMatchQuery _query;

        public MatchesController(IMatchQuery query)
        {
            _query = query;
        }

        /// <summary>
        ///     Find a match.
        /// </summary>
        [HttpGet("{matchId}", Name = nameof(FindMatchAsync))]
        public async Task<IActionResult> FindMatchAsync(MatchId matchId)
        {
            var match = await _query.FindMatchAsync(matchId);

            return match
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("Match not found."))
                .Single();
        }
    }
}