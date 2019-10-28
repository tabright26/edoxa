// Filename: GamesController.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Games.Api.Areas.Games.Responses;
using eDoxa.Arena.Games.Domain.Services;
using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Games.Api.Areas.Games.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/games")]
    [ApiExplorerSettings(GroupName = "Games")]
    public sealed class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GamesController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GameResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync()
        {
            var userId = HttpContext.GetUserId();

            var gameInfos = await _gameService.FetchGameInfosAsync(userId);

            return this.Ok(_mapper.Map<IEnumerable<GameResponse>>(gameInfos));
        }

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
