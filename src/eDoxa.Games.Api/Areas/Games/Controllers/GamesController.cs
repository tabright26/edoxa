// Filename: GameOptionsController.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Api.Infrastructure;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace eDoxa.Games.Api.Areas.Games.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/games")]
    [ApiExplorerSettings(GroupName = "Games")]
    public sealed class GamesController : ControllerBase
    {
        private readonly IGameCredentialService _gameCredentialService;

        public GamesController(IGameCredentialService gameCredentialService, IOptions<GamesOptions> options)
        {
            _gameCredentialService = gameCredentialService;
            Options = options.Value;
        }

        private GamesOptions Options { get; }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            // TODO: Temp.
            Options.LeagueOfLegends.Verified = await _gameCredentialService.CredentialExistsAsync(HttpContext.GetUserId(), Game.LeagueOfLegends);

            return this.Ok(Options);
        }
    }
}
