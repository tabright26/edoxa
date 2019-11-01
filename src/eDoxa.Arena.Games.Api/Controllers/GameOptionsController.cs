// Filename: GameOptionsController.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.Api.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace eDoxa.Arena.Games.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/game/options")]
    [ApiExplorerSettings(GroupName = "Game Options")]
    public sealed class GameOptionsController : ControllerBase
    {
        public GameOptionsController(IOptions<GamesOptions> options)
        {
            Options = options.Value;
        }

        private GamesOptions Options { get; }

        [HttpGet]
        public IActionResult GetAsync()
        {
            return this.Ok(Options);
        }
    }
}
