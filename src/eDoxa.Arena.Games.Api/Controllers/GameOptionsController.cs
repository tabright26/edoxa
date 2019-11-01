// Filename: GameOptionsController.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Arena.Games.Api.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Games.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/game/options")]
    [ApiExplorerSettings(GroupName = "Game Options")]
    public sealed class GameOptionsController : ControllerBase
    {
        public GameOptionsController(IOptions<GameOptions> options)
        {
            Options = options.Value;
        }

        private GameOptions Options { get; }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Dictionary<string, GameConfig>))]
        public IActionResult GetAsync()
        {
            return this.Ok(Options.Games);
        }
    }
}
