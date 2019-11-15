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

namespace eDoxa.Games.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/game/options")]
    [ApiExplorerSettings(GroupName = "Game Options")]
    public sealed class GameOptionsController : ControllerBase
    {
        private readonly ICredentialService _credentialService;

        public GameOptionsController(ICredentialService credentialService, IOptions<GamesOptions> options)
        {
            _credentialService = credentialService;
            Options = options.Value;
        }

        private GamesOptions Options { get; }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            // TODO: Temp.
            Options.LeagueOfLegends.Verified = await _credentialService.CredentialExistsAsync(HttpContext.GetUserId(), Game.LeagueOfLegends);

            return this.Ok(Options);
        }
    }
}
