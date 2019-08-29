// Filename: GamesController.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Extensions;
using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Api.Infrastructure.Models;

using IdentityServer4.AccessTokenValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Identity.Api.Areas.Identity.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/games")]
    [ApiExplorerSettings(GroupName = "Games")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public sealed class GamesController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public GamesController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GameResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var responses = await _userManager.GenerateGameResponsesAsync(user);

            return this.Ok(responses);
        }

        [HttpPost("{game}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostAsync(Game game, [FromBody] GamePostRequest model)
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
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
