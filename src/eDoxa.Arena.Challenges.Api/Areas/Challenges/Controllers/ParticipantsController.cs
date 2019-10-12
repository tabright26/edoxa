// Filename: ParticipantsController.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses;
using eDoxa.Arena.Challenges.Api.Infrastructure.Queries.Extensions;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ParticipantResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
        public async Task<IActionResult> GetByIdAsync(ParticipantId participantId)
        {
            if (ModelState.IsValid)
            {
                var response = await _query.FindParticipantResponseAsync(participantId);

                if (response == null)
                {
                    return this.NotFound("Participant not found.");
                }

                return this.Ok(response);
            }

            return this.BadRequest(ModelState);
        }
    }
}
