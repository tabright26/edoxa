// Filename: ChallengeParticipantsController.cs
// Date Created: 2019-04-21
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

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges/{challengeId}/participants")]
    [ApiExplorerSettings(GroupName = "Challenges")]
    public class ChallengeParticipantsController : ControllerBase
    {
        private readonly IParticipantQuery _participantQuery;
        private readonly IMediator _mediator;

        public ChallengeParticipantsController(IParticipantQuery participantQuery, IMediator mediator)
        {
            _participantQuery = participantQuery;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find the participants of a challenge.
        /// </summary>
        [HttpGet(Name = nameof(FindChallengeParticipantsAsync))]
        public async Task<IActionResult> FindChallengeParticipantsAsync(ChallengeId challengeId)
        {
            var participants = await _participantQuery.FindChallengeParticipantsAsync(challengeId);

            return participants
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }

        /// <summary>
        ///     Register a participant to a challenge.
        /// </summary>
        [HttpPost(Name = nameof(RegisterChallengeParticipantAsync))]
        public async Task<IActionResult> RegisterChallengeParticipantAsync(ChallengeId challengeId)
        {
            var either = await _mediator.SendCommandAsync(new RegisterParticipantCommand(challengeId));

            return either.Match<IActionResult>(error => this.BadRequest(error.ToString()), this.Ok);
        }
    }
}