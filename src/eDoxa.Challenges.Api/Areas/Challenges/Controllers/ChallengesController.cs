// Filename: ChallengesController.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Challenges.Api.Infrastructure.Queries.Extensions;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Requests;
using eDoxa.Challenges.Responses;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Security;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Challenges.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenge", IgnoreApi = true)]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeQuery _challengeQuery;
        private readonly IChallengeService _challengeService;

        public ChallengesController(IChallengeQuery challengeQuery, IChallengeService challengeService)
        {
            _challengeQuery = challengeQuery;
            _challengeService = challengeService;
        }

        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation("Fetch challenges.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeResponse[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> GetAsync(Game? game = null, ChallengeState? state = null)
        {
            var responses = await _challengeQuery.FetchChallengeResponsesAsync(game, state);

            if (!responses.Any())
            {
                return this.NoContent();
            }

            return this.Ok(responses);
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost(Name = "CreateChallenge")]
        [SwaggerOperation("Create a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeResponse))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> PostAsync([FromBody] CreateChallengeRequest request)
        {
            var challengeId = ChallengeId.FromGuid(request.ChallengeId);

            var result = await _challengeService.CreateChallengeAsync(
                challengeId,
                new ChallengeName(request.Name),
                Game.FromName(request.Game),
                new BestOf(request.BestOf),
                new Entries(request.Entries),
                new ChallengeDuration(TimeSpan.FromDays(request.Duration)),
                new UtcNowDateTimeProvider());

            if (result.IsValid)
            {
                var response = await _challengeQuery.FindChallengeResponseAsync(challengeId);

                return this.Ok(response);

                //Todo: This cause error in the test, try to check if it breaks on the real version.
                //return this.Created(Url.Link("CreateChallenge", null), response);
            }

            result.AddToModelState(ModelState, null);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [AllowAnonymous]
        [HttpGet("{challengeId}")]
        [SwaggerOperation("Find a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(ChallengeId challengeId)
        {
            var response = await _challengeQuery.FindChallengeResponseAsync(challengeId);

            if (response == null)
            {
                return this.NotFound("Challenge not found.");
            }

            return this.Ok(response);
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost("{challengeId}")]
        [SwaggerOperation("Synchronize a challenge.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostByIdAsync(ChallengeId challengeId)
        {
            var challenge = await _challengeService.FindChallengeAsync(challengeId);

            if (challenge == null)
            {
                return this.NotFound("Challenge not found.");
            }

            var result = await _challengeService.SynchronizeChallengeAsync(challenge, new UtcNowDateTimeProvider());

            if (result.IsValid)
            {
                var response = await _challengeQuery.FindChallengeResponseAsync(challengeId);

                return this.Ok(response);
            }

            result.AddToModelState(ModelState, null);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
