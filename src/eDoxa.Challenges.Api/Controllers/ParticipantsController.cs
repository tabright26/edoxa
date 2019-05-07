// Filename: ParticipantsController.cs
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

using eDoxa.Challenges.Domain.Entities.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/participants")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantQueries _queries;

        public ParticipantsController(IParticipantQueries queries)
        {
            _queries = queries;
        }

        /// <summary>
        ///     Find a participant.
        /// </summary>
        [HttpGet("{participantId}", Name = nameof(FindParticipantAsync))]
        public async Task<IActionResult> FindParticipantAsync(ParticipantId participantId)
        {
            var participant = await _queries.FindParticipantAsync(participantId);

            return participant
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("Participant not found."))
                .Single();
        }
    }
}