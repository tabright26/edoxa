// Filename: ChallengesController.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Challenges.Responses;
using eDoxa.Cashier.Api.Infrastructure.Queries.Extensions;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Areas.Challenges.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public sealed class ChallengesController : ControllerBase
    {
        private readonly IChallengeQuery _challengeQuery;

        public ChallengesController(IChallengeQuery challengeQuery)
        {
            _challengeQuery = challengeQuery;
        }

        /// <summary>
        ///     Find a challenge.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{challengeId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(ChallengeId challengeId)
        {
            if (ModelState.IsValid)
            {
                var response = await _challengeQuery.FindChallengeResponseAsync(challengeId);

                if (response == null)
                {
                    return this.NotFound("Challenge not found.");
                }

                return this.Ok(response);
            }

            return this.BadRequest(ModelState);
        }
    }
}
