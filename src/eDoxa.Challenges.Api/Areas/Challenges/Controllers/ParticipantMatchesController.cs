// Filename: ParticipantMatchesController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Infrastructure.Queries.Extensions;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Challenges.Api.Areas.Challenges.Controllers
{
    [Authorize]
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

        [HttpGet]
        [SwaggerOperation("Find the matches of a participant.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(MatchDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
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
