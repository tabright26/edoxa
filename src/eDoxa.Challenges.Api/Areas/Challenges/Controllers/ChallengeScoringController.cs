// Filename: ChallengeScoringController.cs
// Date Created: 2019-11-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.HttpClients;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/challenges/{game}/scoring")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengeScoringController : ControllerBase
    {
        private readonly IGamesHttpClient _httpClient;

        public ChallengeScoringController(IGamesHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(Game game)
        {
            var scoring = await _httpClient.GetChallengeScoringAsync(game);

            return this.Ok(scoring);
        }

        [HttpGet("matches")]
        public async Task<IActionResult> PostAsync(Game game, [FromQuery] string playerId, [FromQuery] DateTime? startedAt, [FromQuery] DateTime? endedAt)
        {
            var scoring = await _httpClient.GetChallengeMatchesAsync(game, playerId, startedAt, endedAt);

            return this.Ok(scoring);
        }
    }
}
