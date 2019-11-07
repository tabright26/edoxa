// Filename: ChallengesController.cs
// Date Created: 2019-11-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Web.Aggregator.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Web.Aggregator.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/challenges/{game}/scoring")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public class ChallengesController : ControllerBase
    {
        private readonly IGamesService _service;

        public ChallengesController(IGamesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(Game game)
        {
            var scoring = await _service.GetChallengeScoringAsync(game);

            return this.Ok(scoring);
        }
    }
}
