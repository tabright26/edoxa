// Filename: ChallengesController.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Cashier.Api.Infrastructure.Queries.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Requests;
using eDoxa.Cashier.Responses;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Security;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenges")]
    public sealed class ChallengesController : ControllerBase
    {
        private readonly IChallengeQuery _challengeQuery;
        private readonly IChallengeService _challengeService;

        public ChallengesController(IChallengeQuery challengeQuery, IChallengeService challengeService)
        {
            _challengeQuery = challengeQuery;
            _challengeService = challengeService;
        }

        /// <summary>
        ///     Find a challenge.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _challengeQuery.FetchChallengeResponsesAsync();

            if (!response.Any())
            {
                return this.NoContent();
            }

            return this.Ok(response);
        }

        /// <summary>
        ///     Create a challenge.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = AppRoles.Admin)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostAsync(CreateChallengeRequest request)
        {
            var challengeId = ChallengeId.FromGuid(request.ChallengeId);

            var result = await _challengeService.CreateChallengeAsync(
                challengeId,
                new PayoutEntries(request.PayoutEntries),
                new EntryFee(request.EntryFeeAmount, Currency.FromName(request.EntryFeeCurrency)));

            if (result.IsValid)
            {
                var response = await _challengeQuery.FindChallengeResponseAsync(challengeId);

                return this.Ok(response);
            }

            result.AddToModelState(ModelState, null);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        /// <summary>
        ///     Find a challenge.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{challengeId}")]
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
    }
}
