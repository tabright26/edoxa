// Filename: CredentialsController.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Games.Api.Areas.Credentials.Responses;
using eDoxa.Arena.Games.Api.Areas.Credentials.Services.Abstractions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Games.Api.Areas.Credentials.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/credentials")]
    [ApiExplorerSettings(GroupName = "Credentials")]
    public sealed class CredentialsController : ControllerBase
    {
        private readonly ICredentialService _credentialService;
        private readonly IMapper _mapper;

        public CredentialsController(ICredentialService credentialService, IMapper mapper)
        {
            _credentialService = credentialService;
            _mapper = mapper;
        }

        /// <summary>
        /// </summary>
        [HttpGet("{game}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CredentialResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByGameAsync(Game game)
        {
            var credential = await _credentialService.FindCredentialAsync(HttpContext.GetUserId(), game);

            if (credential == null)
            {
                return this.NotFound($"The user's {game} credential was not found.");
            }

            return this.Ok(_mapper.Map<CredentialResponse>(credential));
        }

        /// <summary>
        /// </summary>
        [HttpPost("{game}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CredentialResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostByGameAsync(Game game)
        {
            var userId = HttpContext.GetUserId();

            var result = await _credentialService.LinkCredentialAsync(userId, game);

            if (result.IsValid)
            {
                var credential = await _credentialService.FindCredentialAsync(userId, game);

                return this.Ok(_mapper.Map<CredentialResponse>(credential));
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        /// </summary>
        [HttpDelete("{game}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CredentialResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteByGameAsync(Game game)
        {
            var credential = await _credentialService.FindCredentialAsync(HttpContext.GetUserId(), game);

            if (credential == null)
            {
                return this.NotFound($"The user's {game} credential was not found.");
            }

            var result = await _credentialService.UnlinkCredentialAsync(credential);

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<CredentialResponse>(credential));
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }
    }
}
