// Filename: ChallengeGameMatchesController.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Games.Api.Areas.Challenge.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/challenge/games/{game}/matches")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengeGameMatchesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return this.Ok();
        }
    }
}
