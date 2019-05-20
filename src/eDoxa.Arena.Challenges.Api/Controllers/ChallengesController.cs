// Filename: ChallengesController.cs
// Date Created: 2019-05-06
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Seedwork.Domain.Enumerations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenges")]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeQueries _queries;

        public ChallengesController(IChallengeQueries queries)
        {
            _queries = queries;
        }

        /// <summary>
        ///     Find the challenges.
        /// </summary>
        [HttpGet(Name = nameof(FindChallengesAsync))]
        public async Task<IActionResult> FindChallengesAsync(Game game, ChallengeType type, ChallengeState state)
        {
            game = game ?? Game.All;

            type = type ?? ChallengeType.All;

            state = state ?? ChallengeState.All;

            var challenges = await _queries.FindChallengesAsync(game, type, state);

            return challenges
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }

        /// <summary>
        ///     Find a challenge.
        /// </summary>
        [HttpGet("{challengeId}", Name = nameof(FindChallengeAsync))]
        public async Task<IActionResult> FindChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await _queries.FindChallengeAsync(challengeId);

            return challenge
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("Challenge not found."))
                .Single();
        }
    }
}