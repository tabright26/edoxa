﻿// Filename: ChallengesController.cs
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

using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Seedwork.Domain.Enumerations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static eDoxa.Seedwork.Application.Extensions.Enumeration;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges")]
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
        public async Task<IActionResult> FindChallengesAsync(string type, string game, string state)
        {
            var challenges = await _queries.FindChallengesAsync(FromAnyDisplayName<ChallengeType>(type), FromAnyDisplayName<Game>(game), FromAnyDisplayName<ChallengeState>(state));

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