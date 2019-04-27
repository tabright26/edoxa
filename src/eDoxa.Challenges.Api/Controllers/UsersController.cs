// Filename: UsersController.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Seedwork.Domain.Common.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IChallengeQueries _queries;

        public UsersController(IChallengeQueries queries)
        {
            _queries = queries;
        }

        /// <summary>
        ///     Find the challenge history of a user.
        /// </summary>
        [HttpGet("{userId}/challenge-history", Name = nameof(FindUserChallengeHistoryAsync))]
        public async Task<IActionResult> FindUserChallengeHistoryAsync(
            UserId userId,
            Game game = Game.All,
            ChallengeType type = ChallengeType.All,
            ChallengeState1 state = ChallengeState1.All)
        {
            var challenges = await _queries.FindUserChallengeHistoryAsync(userId, game, type, state);

            return challenges
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}