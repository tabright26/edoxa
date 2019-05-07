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

using eDoxa.Challenges.Domain.Entities.AggregateModels.MatchAggregate;
using eDoxa.Challenges.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/matches")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchQueries _queries;

        public MatchesController(IMatchQueries queries)
        {
            _queries = queries;
        }

        /// <summary>
        ///     Find a match.
        /// </summary>
        [HttpGet("{matchId}", Name = nameof(FindMatchAsync))]
        public async Task<IActionResult> FindMatchAsync(MatchId matchId)
        {
            var match = await _queries.FindMatchAsync(matchId);

            return match
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("Match not found."))
                .Single();
        }
    }
}