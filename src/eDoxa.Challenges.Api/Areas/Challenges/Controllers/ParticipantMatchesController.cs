// Filename: ParticipantMatchesController.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Infrastructure.Queries.Extensions;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Responses;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Challenges.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/participants/{participantId}/matches")]
    [ApiExplorerSettings(GroupName = "Participant")]
    public class ParticipantMatchesController : ControllerBase
    {
        private readonly IMatchQuery _matchQuery;

        public ParticipantMatchesController(IMatchQuery matchQuery)
        {
            _matchQuery = matchQuery;
        }

        /// <summary>
        ///     Find the matches of a participant.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(MatchResponse[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> GetAsync(ParticipantId participantId)
        {
            var responses = await _matchQuery.FetchParticipantMatchResponsesAsync(participantId);

            if (!responses.Any())
            {
                return this.NoContent();
            }

            return this.Ok(responses);
        }
    }
}
