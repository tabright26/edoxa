// Filename: ChallengesController.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Application.Commands;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Commands.Extensions;
using eDoxa.Security;
using eDoxa.Seedwork.Domain.Enumerations;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenges")]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeQuery _challengeQuery;
        private readonly IMediator _mediator;

        public ChallengesController(IChallengeQuery challengeQuery, IMediator mediator)
        {
            _challengeQuery = challengeQuery;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get challenges.
        /// </summary>
        [HttpGet(Name = nameof(FindChallengesAsync))]
        public async Task<IActionResult> FindChallengesAsync([FromQuery] Game game)
        {
            var challenges = await _challengeQuery.FindChallengesAsync(game);

            return challenges.Select(this.Ok).Cast<IActionResult>().DefaultIfEmpty(this.NoContent()).Single();
        }

        /// <summary>
        ///     Create a challenge - Administrator only.
        /// </summary>
        [Authorize(Roles = CustomRoles.Administrator)]
        [HttpPost(Name = nameof(CreateChallenge))]
        public async Task<IActionResult> CreateChallenge([FromBody] CreateChallengeCommand command)
        {
            var either = await _mediator.SendCommandAsync(command);

            return either.Match<IActionResult>(error => this.BadRequest(error.ToString()), this.Ok);
        }

        /// <summary>
        ///     Find a challenge.
        /// </summary>
        [HttpGet("{challengeId}", Name = nameof(FindChallengeAsync))]
        public async Task<IActionResult> FindChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await _challengeQuery.FindChallengeAsync(challengeId);

            return challenge.Select(this.Ok).Cast<IActionResult>().DefaultIfEmpty(this.NotFound("Challenge not found.")).Single();
        }
    }
}
