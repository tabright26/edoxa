// Filename: ChallengesController.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Queries.Extensions;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Controllers
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
        [HttpGet("{challengeId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ChallengeViewModel))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetByIdAsync(ChallengeId challengeId)
        {
            var challengeViewModel = await _challengeQuery.FindChallengeViewModelAsync(challengeId);

            if (challengeViewModel == null)
            {
                return this.NotFound("Challenge not found.");
            }

            return this.Ok(challengeViewModel);
        }
    }
}
