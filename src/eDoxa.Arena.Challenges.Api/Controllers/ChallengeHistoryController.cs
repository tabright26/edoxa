﻿// Filename: ChallengeHistoryController.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
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
    [Route("api/challenges/history")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengeHistoryController : ControllerBase
    {
        private readonly IChallengeQuery _challengeQuery;

        public ChallengeHistoryController(IChallengeQuery challengeQuery)
        {
            _challengeQuery = challengeQuery;
        }

        /// <summary>
        ///     Find the challenge history of a user.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChallengeViewModel>))]
        public async Task<IActionResult> GetAsync(ChallengeGame game = null, ChallengeState state = null)
        {
            var challenges = await _challengeQuery.FindUserChallengeHistoryAsync(game, state);

            if (!challenges.Any())
            {
                return this.NoContent();
            }

            return this.Ok(challenges);
        }
    }
}
