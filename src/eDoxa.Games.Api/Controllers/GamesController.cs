// Filename: GamesController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Api.Infrastructure;
using eDoxa.Games.Domain.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Games.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/games")]
    [ApiExplorerSettings(GroupName = "Games")]
    public sealed class GamesController : ControllerBase
    {
        private readonly IGameCredentialService _gameCredentialService;
        private readonly IOptions<GamesOptions> _optionsSnapshot;

        public GamesController(IGameCredentialService gameCredentialService, IOptionsSnapshot<GamesOptions> optionsSnapshot)
        {
            _gameCredentialService = gameCredentialService;
            _optionsSnapshot = optionsSnapshot;
        }

        private GamesOptions Options => _optionsSnapshot.Value;

        [HttpGet]
        [SwaggerOperation("Unlink game credential.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GamesOptions))]
        public async Task<IActionResult> GetAsync()
        {
            // TODO: Temp.
            Options.LeagueOfLegends.Verified = await _gameCredentialService.CredentialExistsAsync(HttpContext.GetUserId(), Game.LeagueOfLegends);

            return this.Ok(Options);
        }
    }
}
