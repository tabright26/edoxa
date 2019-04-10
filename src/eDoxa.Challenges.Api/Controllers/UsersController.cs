// Filename: UsersController.cs
// Date Created: 2019-03-18
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Properties;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Seedwork.Domain.Common.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IChallengeQueries _queries;

        public UsersController(ILogger<UsersController> logger, IChallengeQueries queries)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        /// <summary>
        ///     Find the challenge history of a user.
        /// </summary>
        [HttpGet("{userId}/challenge-history", Name = nameof(FindUserChallengeHistoryAsync))]
        public async Task<IActionResult> FindUserChallengeHistoryAsync(
            UserId userId,
            Game game = Game.All,
            ChallengeType type = ChallengeType.All,
            ChallengeState state = ChallengeState.All)
        {
            try
            {
                var challenges = await _queries.FindUserChallengeHistoryAsync(userId, game, type, state);

                if (!challenges.Any())
                {
                    return this.NoContent();
                }

                return this.Ok(challenges);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.UsersController_Error_FindUserChallengeHistoryAsync);
            }

            return this.BadRequest(Resources.UsersController_BadRequest_FindUserChallengeHistoryAsync);
        }
    }
}