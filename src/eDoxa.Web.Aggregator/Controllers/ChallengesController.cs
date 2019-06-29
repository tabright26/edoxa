// Filename: ChallengesController.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Web.Aggregator.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Web.Aggregator.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public class ChallengesController : ControllerBase
    {
        private readonly IArenaChallengesService _arenaChallengesService;
        private readonly IIdentityService _identityService;

        public ChallengesController(IArenaChallengesService arenaChallengesService, IIdentityService identityService)
        {
            _arenaChallengesService = arenaChallengesService;
            _identityService = identityService;
        }

        /// <summary>
        ///     Get challenges.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            var challenges = await _arenaChallengesService.FetchChallengesAsync();

            if (!challenges.Any())
            {
                return this.NoContent();
            }

            var users = await _identityService.FetchUsersAsync();

            foreach (var challenge in challenges)
            {
                foreach (var participant in challenge.participants)
                {
                    participant.userName = users.SingleOrDefault(user => user.id == participant.userId)?.userName ?? "Inaccessible username";
                }
            }

            return this.Ok(challenges);
        }
    }
}
