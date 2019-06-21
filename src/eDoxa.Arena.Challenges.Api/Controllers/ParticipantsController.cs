// Filename: ParticipantsController.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Abstractions.Queries;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/participants")]
    [ApiExplorerSettings(GroupName = "Participant")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantQuery _query;

        public ParticipantsController(IParticipantQuery query)
        {
            _query = query;
        }

        /// <summary>
        ///     Find a participant.
        /// </summary>
        [HttpGet("{participantId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ParticipantViewModel))]
        public async Task<IActionResult> GetByIdAsync(ParticipantId participantId)
        {
            var participant = await _query.FindParticipantAsync(participantId);

            if (participant == null)
            {
                return this.NotFound("Participant not found.");
            }

            return this.Ok(participant);
        }
    }
}
