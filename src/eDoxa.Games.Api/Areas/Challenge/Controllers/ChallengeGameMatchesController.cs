// Filename: ChallengeGameMatchesController.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Games.Api.Areas.Challenge.Controllers
{
    [Authorize]
    [ApiController]
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

        [HttpGet]
        [AllowAnonymous] // TODO: Quick fix.
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
