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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Security;
using eDoxa.Seedwork.Application.Commands.Extensions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using JetBrains.Annotations;

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
        [HttpGet(Name = nameof(GetChallengesAsync))]
        public async Task<IActionResult> GetChallengesAsync([CanBeNull] Game game, [CanBeNull] ChallengeState state)
        {
            var challenges = await _challengeQuery.GetChallengesAsync(game, state);

            if (!challenges.Any())
            {
                return this.NoContent();
            }

            return this.Ok(challenges);
        }

        /// <summary>
        ///     Create a challenge - Administrator only.
        /// </summary>
        [Authorize(Roles = CustomRoles.Administrator)]
        [HttpPost(Name = nameof(CreateChallenge))]
        public async Task<IActionResult> CreateChallenge([FromBody] CreateChallengeCommand command)
        {
            var challenge = await _mediator.SendCommandAsync(command);

            return this.Ok(challenge);
        }

        /// <summary>
        ///     Find a challenge.
        /// </summary>
        [HttpGet("{challengeId}", Name = nameof(GetChallengeAsync))]
        public async Task<IActionResult> GetChallengeAsync(ChallengeId challengeId)
        {
            var challenge = await _challengeQuery.GetChallengeAsync(challengeId);

            if (challenge == null)
            {
                return this.NotFound("Challenge not found.");
            }

            return this.Ok(challenge);
        }

        /// <summary>
        ///     Complete a challenge - Administrator only.
        /// </summary>
        [Authorize(Roles = CustomRoles.Administrator)]
        [HttpPost("{challengeId}/close", Name = nameof(CloseChallengeAsync))]
        public async Task<IActionResult> CloseChallengeAsync(ChallengeId challengeId)
        {
            await _mediator.SendCommandAsync(new CloseChallengeCommand(challengeId));

            return this.Ok();
        }

        /// <summary>
        ///     Synchronize a challenge - Administrator only.
        /// </summary>
        [Authorize(Roles = CustomRoles.Administrator)]
        [HttpPost("{challengeId}/synchronize", Name = nameof(SynchronizeChallengeAsync))]
        public async Task<IActionResult> SynchronizeChallengeAsync(ChallengeId challengeId)
        {
            await _mediator.SendCommandAsync(new SynchronizeChallengeCommand(challengeId));

            return this.Ok();
        }
    }
}
