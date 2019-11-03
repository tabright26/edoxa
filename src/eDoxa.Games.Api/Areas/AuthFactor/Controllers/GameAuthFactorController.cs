// Filename: GameAuthFactorController.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Games.Api.Areas.AuthFactor.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{game}/auth-factor")]
    [ApiExplorerSettings(GroupName = "Game")]
    public sealed class GameAuthFactorController : ControllerBase
    {
        private readonly IAuthFactorService _authFactorService;

        public GameAuthFactorController(IAuthFactorService authFactorService)
        {
            _authFactorService = authFactorService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Game game, [FromBody] object request)
        {
            var userId = HttpContext.GetUserId();

            var result = await _authFactorService.GenerateAuthFactorAsync(userId, game, request);

            if (result.IsValid)
            {
                var authFactor = await _authFactorService.FindAuthFactorAsync(userId, game);

                return this.Ok(authFactor);
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }
    }
}
