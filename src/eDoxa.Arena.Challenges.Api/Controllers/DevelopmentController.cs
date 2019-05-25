// Filename: DevelopmentController.cs
// Date Created: 2019-05-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Application.Commands;
using eDoxa.Commands.Extensions;
using eDoxa.Security;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "Development")]
    public class DevelopmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DevelopmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Create a money challenge - Administrator only.
        /// </summary>
        [Authorize(Roles = CustomRoles.Administrator)]
        [HttpPost("create-challenge-with-money", Name = nameof(CreateMoneyChallenge))]
        public async Task<IActionResult> CreateMoneyChallenge([FromBody] CreateMoneyChallengeCommand command)
        {
            var either = await _mediator.SendCommandAsync(command);

            return either.Match<IActionResult>(error => this.BadRequest(error.ToString()), this.Ok);
        }

        /// <summary>
        ///     Create a token challenge - Administrator only.
        /// </summary>
        [Authorize(Roles = CustomRoles.Administrator)]
        [HttpPost("create-challenge-with-token", Name = nameof(CreateTokenChallenge))]
        public async Task<IActionResult> CreateTokenChallenge([FromBody] CreateTokenChallengeCommand command)
        {
            var either = await _mediator.SendCommandAsync(command);

            return either.Match<IActionResult>(error => this.BadRequest(error.ToString()), this.Ok);
        }
    }
}
