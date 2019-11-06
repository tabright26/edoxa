// Filename: ChallengeParticipantsController.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Responses;
using eDoxa.Challenges.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Challenges.Api.Infrastructure.Queries.Extensions;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
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
    [Route("api/challenges/{challengeId}/participants")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public class ChallengeParticipantsController : ControllerBase
    {
        private readonly IParticipantQuery _participantQuery;
        private readonly IChallengeService _challengeService;

        public ChallengeParticipantsController(IParticipantQuery participantQuery, IChallengeService challengeService)
        {
            _participantQuery = participantQuery;
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
            var challenge = await _challengeService.FindChallengeAsync(challengeId);

            if (challenge == null)
            {
                return this.NotFound("Challenge not found.");
            }

            await _challengeService.RegisterChallengeParticipantAsync(
                challenge,
                HttpContext.GetUserId(),
                HttpContext.GetPlayerId(challenge.Game),
                new UtcNowDateTimeProvider());

            return this.Ok("Participant as been registered.");
        }
    }
}
