﻿// Filename: MatchesController.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Api.Infrastructure.Queries.Extensions;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Responses;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Challenges.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(MatchResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(MatchId matchId)
        {
            var response = await _matchQuery.FindMatchResponseAsync(matchId);

            if (response == null)
            {
                return this.NotFound("Match not found.");
            }

            return this.Ok(response);
        }
    }
}