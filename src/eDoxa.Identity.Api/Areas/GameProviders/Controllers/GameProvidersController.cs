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

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Identity.Api.Areas.GameProviders.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/game-providers")]
    [ApiExplorerSettings(GroupName = "Game Providers")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
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

        [HttpPost("{game}")]
        public async Task<IActionResult> PostAsync(Game game, [FromBody] AddGameProviderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var result = await _userManager.AddGameProviderAsync(user, new UserGameProviderInfo(game, model.PlayerId));

                if (result.Succeeded)
                {
                    return this.Ok($"The user's game provider for {game} has been added.");
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

                var result = await _userManager.RemoveGameProviderAsync(user, game);

                if (result.Succeeded)
                {
                    return this.Ok($"The user's game provider for {game} has been removed.");
                }

                ModelState.Bind(result);
            }

            return this.BadRequest(ModelState);
        }
    }
}
