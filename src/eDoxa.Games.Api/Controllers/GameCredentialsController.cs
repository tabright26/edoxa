﻿// Filename: GameCredentialsController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Games.Domain.Services;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Games.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/games/{game}/credentials")]
    [ApiExplorerSettings(GroupName = "Credential")]
    public sealed class GameCredentialsController : ControllerBase
    {
        private readonly IGameCredentialService _gameCredentialService;
        private readonly IMapper _mapper;

        public GameCredentialsController(IGameCredentialService gameCredentialService, IMapper mapper)
        {
            _gameCredentialService = gameCredentialService;
            _mapper = mapper;
        }

        [HttpDelete]
        [SwaggerOperation("Unlink game credential.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GameCredentialDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> UnlinkCredentialAsync(Game game)
        {
            var credential = await _gameCredentialService.FindCredentialAsync(HttpContext.GetUserId(), game);

            if (credential == null)
            {
                return this.NotFound($"The user's {game} credential was not found.");
            }

            var result = await _gameCredentialService.UnlinkCredentialAsync(credential);

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<GameCredentialDto>(credential));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
