// Filename: GameProvidersController.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Application;
using eDoxa.Identity.Api.Application.Managers;
using eDoxa.Identity.Api.Areas.GameProviders.ViewModels;
using eDoxa.Identity.Api.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Areas.GameProviders.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/game-providers")]
    [ApiExplorerSettings(GroupName = "Game")]
    public sealed class GameProvidersController : ControllerBase
    {
        private readonly CustomUserManager _userManager;

        public GameProvidersController(CustomUserManager userManager)
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

            var gameProviderLinks = await _userManager.GetGameProviderLinksAsync(user);

            return this.Ok(gameProviderLinks);
        }

        [HttpPost("{gameProvider}")]
        public async Task<IActionResult> PostAsync(GameProvider gameProvider, [FromBody] AddGameProviderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var result = await _userManager.AddGameProviderAsync(user, new UserGameProviderInfo(gameProvider, model.ProviderKey));

                if (result.Succeeded)
                {
                    return this.Ok($"The user's game provider for {gameProvider} has been added.");
                }

                ModelState.Bind(result);
            }

            return this.BadRequest(ModelState);
        }

        [HttpDelete("{gameProvider}")]
        public async Task<IActionResult> DeleteAsync(GameProvider gameProvider)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var result = await _userManager.RemoveGameProviderAsync(user, gameProvider);

                if (result.Succeeded)
                {
                    return this.Ok($"The user's game provider for {gameProvider} has been removed.");
                }

                ModelState.Bind(result);
            }

            return this.BadRequest(ModelState);
        }
    }
}
