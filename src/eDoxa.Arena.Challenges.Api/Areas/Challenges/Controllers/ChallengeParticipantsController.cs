// Filename: ChallengeParticipantsController.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses;
using eDoxa.Arena.Challenges.Api.Infrastructure.Queries.Extensions;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Seedwork.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/challenges/{challengeId}/participants")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public class ChallengeParticipantsController : ControllerBase
    {
        private readonly IParticipantQuery _participantQuery;
        private readonly IChallengeQuery _challengeQuery;
        private readonly IChallengeService _challengeService;

        public ChallengeParticipantsController(IParticipantQuery participantQuery, IChallengeQuery challengeQuery, IChallengeService challengeService)
        {
            _participantQuery = participantQuery;
            _challengeQuery = challengeQuery;
            _challengeService = challengeService;
        }

        /// <summary>
        ///     Find the participants of a challenge.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ParticipantResponse>))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> GetAsync(ChallengeId challengeId)
        {
            if (ModelState.IsValid)
            {
                var responses = await _participantQuery.FetchChallengeParticipantResponsesAsync(challengeId);

                if (!responses.Any())
                {
                    return this.NoContent();
                }

                return this.Ok(responses);
            }

            return this.BadRequest(ModelState);
        }

        /// <summary>
        ///     Register a participant to a challenge.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostAsync(ChallengeId challengeId)
        {
            var userId = HttpContext.GetUserId();

            var challenge = await _challengeQuery.FindChallengeAsync(challengeId);

            if (challenge == null)
            {
                return this.NotFound("Challenge not found.");
            }

            await _challengeService.RegisterParticipantAsync(challengeId, userId, new UtcNowDateTimeProvider());

            return this.Ok("Participant as been registered.");
        }
    }
}
