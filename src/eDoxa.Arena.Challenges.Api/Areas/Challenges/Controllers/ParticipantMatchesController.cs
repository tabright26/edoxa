// Filename: ParticipantMatchesController.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Queries.Extensions;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/participants/{participantId}/matches")]
    [ApiExplorerSettings(GroupName = "Participant")]
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
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<MatchViewModel>))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync(ParticipantId participantId)
        {
            var matchViewModels = await _query.FetchParticipantMatchViewModelsAsync(participantId);

            if (!matchViewModels.Any())
            {
                return this.NoContent();
            }

            return this.Ok(matchViewModels);
        }
    }
}
