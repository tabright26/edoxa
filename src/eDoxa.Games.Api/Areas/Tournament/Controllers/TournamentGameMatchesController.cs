// Filename: TournamentGameMatchesController.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Games.Api.Areas.Tournament.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/tournament/games/{game}/matches")]
    [ApiExplorerSettings(GroupName = "Tournament")]
    public sealed class TournamentGameMatchesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return await Task.FromResult(this.Ok());
        }
    }
}
