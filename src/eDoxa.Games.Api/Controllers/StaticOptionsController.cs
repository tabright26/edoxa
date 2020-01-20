// Filename: StaticOptionsController.cs
// Date Created: 2020-01-19
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Games.Options;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Games.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/static/options")]
    [ApiExplorerSettings(GroupName = "Static")]
    public sealed class StaticOptionsController : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation("Games static options.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GamesApiOptions.Types.StaticOptions))]
        public async Task<IActionResult> GetAsync([FromServices] IOptionsSnapshot<GamesApiOptions> snapshot)
        {
            return await Task.FromResult(this.Ok(snapshot.Value.Static));
        }
    }
}
