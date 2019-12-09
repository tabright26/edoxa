// Filename: ChallengeGameMatchesController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Games.Domain.Services;
using eDoxa.Seedwork.Domain.Dtos;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Games.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/challenge/games/{game}/matches")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengeGameMatchesController : ControllerBase
    {
        private readonly IChallengeService _challengeService;

        public ChallengeGameMatchesController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [AllowAnonymous] // TODO: Quick fix.
        [HttpGet]
        [SwaggerOperation("Get matches for challenge participants.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(MatchDto[]))]
        public async Task<IActionResult> GetAsync(
            Game game,
            [FromQuery] string playerId,
            [FromQuery] DateTime? startedAt,
            [FromQuery] DateTime? endedAt
        )
        {
            var matches = await _challengeService.GetMatchesAsync(
                game,
                PlayerId.Parse(playerId),
                startedAt,
                endedAt);

            return this.Ok(matches);
        }
    }
}
