// Filename: ChallengesController.cs
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

using eDoxa.Arena.Challenges.Api.Infrastructure.Queries.Extensions;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeQuery _challengeQuery;

        public ChallengesController(IChallengeQuery challengeQuery)
        {
            _challengeQuery = challengeQuery;
        }

        /// <summary>
        ///     Get challenges.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChallengeViewModel>))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync(ChallengeGame? game = null, ChallengeState? state = null)
        {
            var challengeViewModels = await _challengeQuery.FetchChallengeViewModelsAsync(game, state);

            if (!challengeViewModels.Any())
            {
                return this.NoContent();
            }

            return this.Ok(challengeViewModels);
        }

        /// <summary>
        ///     Find a challenge.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{challengeId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(ChallengeId challengeId)
        {
            var challengeViewModel = await _challengeQuery.FindChallengeViewModelAsync(challengeId);

            if (challengeViewModel == null)
            {
                return this.NotFound("Challenge not found.");
            }

            return this.Ok(challengeViewModel);
        }
    }
}
