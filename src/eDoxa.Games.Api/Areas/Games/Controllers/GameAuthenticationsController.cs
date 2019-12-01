﻿// Filename: GameAuthenticationsController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Api.Areas.Games.Responses;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Games.Api.Areas.Games.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/games/{game}/authentications")]
    [ApiExplorerSettings(GroupName = "Game")]
    public sealed class GameAuthenticationsController : ControllerBase
    {
        private readonly IGameAuthenticationService _gameAuthenticationService;
        private readonly IGameCredentialService _gameCredentialService;
        private readonly IMapper _mapper;

        public GameAuthenticationsController(IGameAuthenticationService gameAuthenticationService, IGameCredentialService gameCredentialService, IMapper mapper)
        {
            _gameAuthenticationService = gameAuthenticationService;
            _gameCredentialService = gameCredentialService;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerOperation("Generate game authentication.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(object))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync(Game game, [FromBody] object request)
        {
            var userId = HttpContext.GetUserId();

            var result = await _gameAuthenticationService.GenerateAuthenticationAsync(userId, game, request);

            if (result.IsValid)
            {
                var authentication = await _gameAuthenticationService.FindAuthenticationAsync(userId, game);

                return this.Ok(authentication.Factor);
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPut]
        [SwaggerOperation("Validate game authentication.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CredentialResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PutAsync(Game game)
        {
            var userId = HttpContext.GetUserId();

            var result = await _gameCredentialService.LinkCredentialAsync(userId, game);

            if (result.IsValid)
            {
                var credential = await _gameCredentialService.FindCredentialAsync(userId, game);

                return this.Ok(_mapper.Map<CredentialResponse>(credential));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
