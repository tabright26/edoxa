﻿// Filename: ChallengeParticipantsController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Challenges.Api.Infrastructure.Queries.Extensions;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Requests;
using eDoxa.Challenges.Responses;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Challenges.Api.Areas.Challenges.Controllers
{
    [Authorize]
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

        [HttpGet]
        [SwaggerOperation("Find the participants of a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ParticipantResponse[]))]
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

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost]
        [SwaggerOperation("Register a participant to a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ParticipantResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostAsync(ChallengeId challengeId, [FromBody] RegisterChallengeParticipantRequest request)
        {
            var participantId = ParticipantId.FromGuid(request.ParticipantId);

            var challenge = await _challengeService.FindChallengeAsync(challengeId);

            if (challenge == null)
            {
                return this.NotFound("Challenge not found.");
            }

            var playerId = HttpContext.GetPlayerId(challenge.Game);

            var result = await _challengeService.RegisterChallengeParticipantAsync(
                challenge,
                participantId,
                HttpContext.GetUserId(),
                playerId,
                new UtcNowDateTimeProvider());

            if (result.IsValid)
            {
                var response = await _participantQuery.FindParticipantResponseAsync(participantId);

                return this.Ok(response);
            }

            result.AddToModelState(ModelState, null);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
