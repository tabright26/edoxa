// Filename: ChallengeGameScoringController.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
    [Route("api/challenge/games/{game}/scoring")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengeGameScoringController : ControllerBase
    {
        private readonly IChallengeService _challengeService;

        public ChallengeGameScoringController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(Game game)
        {
            var scoring = await _challengeService.GetScoringAsync(game);

            return this.Ok(scoring);
        }
    }
}
