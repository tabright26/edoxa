// Filename: GameCredentialsController.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Api.Areas.Games.Responses;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Games.Api.Areas.Games.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/games/{game}/credentials")]
    [ApiExplorerSettings(GroupName = "Credential")]
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
        ///     Unlink game credential.
        /// </summary>
        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CredentialResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(Game game)
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
