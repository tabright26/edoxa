// Filename: MatchesController.cs
// Date Created: 2019-03-18
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/matches")]
    public class MatchesController : ControllerBase
    {
        private readonly ILogger<MatchesController> _logger;
        private readonly IMatchQueries _queries;

        public MatchesController(ILogger<MatchesController> logger, IMatchQueries queries)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        /// <summary>
        ///     Find a match.
        /// </summary>
        [HttpGet("{matchId}", Name = nameof(FindMatchAsync))]
        public async Task<IActionResult> FindMatchAsync(MatchId matchId)
        {
            try
            {
                var match = await _queries.FindMatchAsync(matchId);

                if (match == null)
                {
                    return this.NotFound(string.Empty);
                }

                return this.Ok(match);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }
    }
}