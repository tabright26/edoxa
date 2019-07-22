// Filename: GamesController.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Extensions;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Areas.Identity.ViewModels;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Api.Infrastructure.Models;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/games")]
    [ApiExplorerSettings(GroupName = "Games")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public sealed class GamesController : ControllerBase
    {
        private readonly CustomUserManager _userManager;

        public GamesController(CustomUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var gameViewModels = await _userManager.GetGameViewModelsAsync(user);

            return this.Ok(gameViewModels);
        }

        [HttpPost("{game}")]
        public async Task<IActionResult> PostAsync(Game game, [FromBody] AddGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var result = await _userManager.AddGameAsync(user, game.Name, model.PlayerId);

                if (result.Succeeded)
                {
                    return this.Ok($"The user's {game} playerId has been added.");
                }

                ModelState.Bind(result);
            }

            return this.BadRequest(ModelState);
        }

        [HttpDelete("{game}")]
        public async Task<IActionResult> DeleteAsync(Game game)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var result = await _userManager.RemoveGameAsync(user, game);

                if (result.Succeeded)
                {
                    return this.Ok($"The user's {game} playerId has been removed.");
                }

                ModelState.Bind(result);
            }

            return this.BadRequest(ModelState);
        }
    }
}
