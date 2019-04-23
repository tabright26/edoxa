// Filename: ChallengesController.cs
// Date Created: 2019-03-18
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Seedwork.Domain.Common.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges")]
    public class ChallengesController : ControllerBase
    {
        private readonly ILogger<ChallengesController> _logger;
        private readonly IChallengeQueries _queries;

        public ChallengesController(ILogger<ChallengesController> logger, IChallengeQueries queries)
        {
            _logger = logger;
            _queries = queries;
        }

        /// <summary>
        ///     Find the challenges.
        /// </summary>
        [HttpGet(Name = nameof(FindChallengesAsync))]
        public async Task<IActionResult> FindChallengesAsync(
            Game game = Game.All,
            ChallengeType type = ChallengeType.All,
            ChallengeState1 state = ChallengeState1.All)
        {
            try
            {
                var challenges = await _queries.FindChallengesAsync(game, type, state);

                if (!challenges.Any())
                {
                    return this.NoContent();
                }

                return this.Ok(challenges);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Find a challenge.
        /// </summary>
        [HttpGet("{challengeId}", Name = nameof(FindChallengeAsync))]
        public async Task<IActionResult> FindChallengeAsync(ChallengeId challengeId)
        {
            try
            {
                var challenge = await _queries.FindChallengeAsync(challengeId);

                if (challenge == null)
                {
                    return this.NotFound(string.Empty);
                }

                return this.Ok(challenge);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }
    }
}