﻿// Filename: ParticipantMatchesController.cs
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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.DTO.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/participants/{participantId}/matches")]
    [ApiExplorerSettings(GroupName = "Participants")]
    public class ParticipantMatchesController : ControllerBase
    {
        private readonly IMatchQuery _query;

        public ParticipantMatchesController(IMatchQuery query)
        {
            _query = query;
        }

        /// <summary>
        ///     Find the matches of a participant.
        /// </summary>
        [HttpGet(Name = nameof(FindParticipantMatchesAsync))]
        public async Task<IActionResult> FindParticipantMatchesAsync(ParticipantId participantId)
        {
            var matches = await _query.FindParticipantMatchesAsync(participantId);

            return matches
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}