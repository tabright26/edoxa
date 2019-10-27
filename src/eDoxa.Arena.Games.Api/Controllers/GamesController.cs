// Filename: GamesController.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Games.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/games")]
    [ApiExplorerSettings(GroupName = "Games")]
    public sealed class GamesController : ControllerBase
    {
        //private readonly IGameCredentialService _userManager;

        //public GamesController(IGameCredentialService userManager)
        //{
        //    _userManager = userManager;
        //}

        //public static async Task<IList<GameResponse>> GenerateGameResponsesAsync(this IUserManager userManager, UserId user)
        //{
        //    var games = await userManager.GetGamesAsync(user);

        //    return Game.GetEnumerations()
        //        .Where(game => game.IsDisplayed)
        //        .Select(
        //            game => new GameResponse
        //            {
        //                Name = game.Name,
        //                IsLinked = games.SingleOrDefault(userGame => Game.FromValue(userGame.Value).Name == game.Name) != null,
        //                IsSupported = game.IsSupported
        //            }
        //        )
        //        .OrderBy(response => response.Name)
        //        .ThenBy(response => response.IsSupported)
        //        .ToList();
        //}

        //[HttpGet]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GameResponse>))]
        //[SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        //public async Task<IActionResult> GetAsync()
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    if (user == null)
        //    {
        //        return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    var responses = await _userManager.GenerateGameResponsesAsync(user);

        //    return this.Ok(responses);
        //}

        //[HttpPost("{game}")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        //[SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        //public async Task<IActionResult> PostAsync(Game game, [FromBody] GamePostRequest model)
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    if (user == null)
        //    {
        //        return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    var result = await _userManager.CreateGameCredentialAsync(user, game, model.PlayerId);

        //    if (result.IsValid)
        //    {
        //        return this.Ok($"The user's {game} playerId has been added.");
        //    }

        //    result.AddToModelState(ModelState, null);

        //    return this.ValidationProblem(ModelState);
        //}

        //[HttpDelete("{game}")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        //[SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        //public async Task<IActionResult> DeleteAsync(Game game)
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    if (user == null)
        //    {
        //        return this.NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    var result = await _userManager.DeleteGameCredentialAsync(user, game);

        //    if (result.IsValid)
        //    {
        //        return this.Ok($"The user's {game} playerId has been removed.");
        //    }

        //    result.AddToModelState(ModelState, null);

        //    return this.ValidationProblem(ModelState);
        //}
    }
}
