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

using eDoxa.Challenges.Application.Commands;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Seedwork.Application.Extensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges/{challengeId}/participants")]
    public class ChallengeParticipantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IParticipantQueries _queries;

        public ChallengeParticipantsController(IParticipantQueries queries, IMediator mediator)
        {
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find the participants of a challenge.
        /// </summary>
        [HttpGet(Name = nameof(FindChallengeParticipantsAsync))]
        public async Task<IActionResult> FindChallengeParticipantsAsync(ChallengeId challengeId)
        {
            var participants = await _queries.FindChallengeParticipantsAsync(challengeId);

            if (!participants.Any())
            {
                return this.NoContent();
            }

            return this.Ok(participants);
        }

        /// <summary>
        ///     Register a participant to a challenge.
        /// </summary>
        [HttpPatch(Name = nameof(RegisterChallengeParticipantAsync))]
        public async Task<IActionResult> RegisterChallengeParticipantAsync([FromBody] RegisterChallengeParticipantCommand command)
        {
            //command.LinkedAccount = User.FindFirst("").Value; // TODO: Create LinkedAccount service.

            await _mediator.SendCommandAsync(command);

            return this.Ok(string.Empty);
        }
    }
}