// Filename: ParticipantMatchesController.cs
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

using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/participants/{participantId}/matches")]
    public class ParticipantMatchesController : ControllerBase
    {
        private readonly IMatchQueries _queries;

        public ParticipantMatchesController(IMatchQueries queries)
        {
            _queries = queries;
        }

        /// <summary>
        ///     Find the matches of a participant.
        /// </summary>
        [HttpGet(Name = nameof(FindParticipantMatchesAsync))]
        public async Task<IActionResult> FindParticipantMatchesAsync(ParticipantId participantId)
        {
            var matches = await _queries.FindParticipantMatchesAsync(participantId);

            return matches
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}