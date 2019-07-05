﻿// Filename: FakeChallengesController.cs
// Date Created: 2019-06-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Commands;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Application.Mvc.Filters.Attributes;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [DevelopmentOnly]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/fake/challenges")]
    [ApiExplorerSettings(GroupName = "Fake")]
    public sealed class FakeChallengesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FakeChallengesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Fake challenges with a random seed - Development only.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> PostAsync([FromBody] FakeChallengesCommand command)
        {
            await _mediator.SendCommandAsync(command);

            return this.Ok("Fake challenges have been seeded.");
        }
    }
}