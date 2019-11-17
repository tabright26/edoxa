// Filename: GameAuthenticationsController.cs
// Date Created: 2019-11-14
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
    [Route("api/games/{game}/authentications")]
    [ApiExplorerSettings(GroupName = "Game")]
    public sealed class GameAuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICredentialService _credentialService;
        private readonly IMapper _mapper;

        public GameAuthenticationsController(IAuthenticationService authenticationService, ICredentialService credentialService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _credentialService = credentialService;
            _mapper = mapper;
        }

        /// <summary>
        ///     Generate game authentication.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostAsync(Game game, [FromBody] object request)
        {
            var userId = HttpContext.GetUserId();

            var result = await _authenticationService.GenerateAuthenticationAsync(userId, game, request);

            if (result.IsValid)
            {
                var authentication = await _authenticationService.FindAuthenticationAsync(userId, game);

                return this.Ok(authentication.Factor);
            }

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        /// <summary>
        ///     Validate game authentication.
        /// </summary>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CredentialResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PutAsync(Game game)
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
    }
}
