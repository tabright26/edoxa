// Filename: OptionsController.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
    [Route("api/options")]
    [ApiExplorerSettings(GroupName = "Options")]
    public sealed class OptionsController : ControllerBase
    {
        public OptionsController(IOptions<GamesOptions> options)
        {
            Options = options.Value;
        }

        private GamesOptions Options { get; }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GamesOptions))]
        public IActionResult GetAsync()
        {
            return this.Ok(Options);
        }
    }
}
