// Filename: CredentialsController.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Games.Domain.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Responses;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Games.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/games/{game}/credentials")]
    [ApiExplorerSettings(GroupName = "Credentials")]
    public sealed class GameCredentialsController : ControllerBase
    {
        private readonly ICredentialService _credentialService;
        private readonly IMapper _mapper;

        public GameCredentialsController(ICredentialService credentialService, IMapper mapper)
        {
            _credentialService = credentialService;
            _mapper = mapper;
        }

        /// <summary>
        /// </summary>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CredentialResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync(Game game)
        {
            var gameCredential = await _credentialService.FindCredentialAsync(HttpContext.GetUserId(), game);

            if (gameCredential == null)
            {
                return this.NotFound($"The user's {game} credential was not found.");
            }

            return this.Ok(_mapper.Map<CredentialResponse>(gameCredential));
        }

        /// <summary>
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CredentialResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync(Game game)
        {
            var userId = HttpContext.GetUserId();

            var result = await _credentialService.LinkCredentialAsync(userId, game);

            if (result.IsValid)
            {
                var gameCredential = await _credentialService.FindCredentialAsync(userId, game);

                return this.Ok(_mapper.Map<CredentialResponse>(gameCredential));
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        /// </summary>
        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CredentialResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(Game game)
        {
            var gameCredential = await _credentialService.FindCredentialAsync(HttpContext.GetUserId(), game);

            if (gameCredential == null)
            {
                return this.NotFound($"The user's {game} credential was not found.");
            }

            var result = await _credentialService.UnlinkCredentialAsync(gameCredential);

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<CredentialResponse>(gameCredential));
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }
    }
}
