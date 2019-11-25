// Filename: ChallengeGameScoringController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Seedwork.Application.Dtos;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Games.Api.Areas.Challenges.Controllers
{
    [Authorize]
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

        [AllowAnonymous] // TODO: Quick fix.
        [HttpGet]
        [SwaggerOperation("Get game challenge scoring.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ScoringDto))]
        public async Task<IActionResult> GetAsync(Game game)
        {
            var scoring = await _challengeService.GetScoringAsync(game);

            return this.Ok(scoring);
        }
    }
}
