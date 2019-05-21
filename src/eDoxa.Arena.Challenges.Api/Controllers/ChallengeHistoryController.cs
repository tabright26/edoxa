// Filename: ChallengeHistoryController.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Security.Abstractions;
using eDoxa.Seedwork.Domain.Enumerations;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges/history")]
    [ApiExplorerSettings(GroupName = "Challenges")]
    public class ChallengeHistoryController : ControllerBase
    {
        private readonly IChallengeQueries _queries;
        private readonly IUserInfoService _userInfoService;

        public ChallengeHistoryController(IUserInfoService userInfoService, IChallengeQueries queries)
        {
            _userInfoService = userInfoService;
            _queries = queries;
        }

        /// <summary>
        ///     Find the challenge history of a user.
        /// </summary>
        [HttpGet(Name = nameof(FindUserChallengeHistoryAsync))]
        public async Task<IActionResult> FindUserChallengeHistoryAsync([CanBeNull] Game game, [CanBeNull] ChallengeState state)
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            game = game ?? Game.All;

            state = state ?? ChallengeState.All;

            var challenges = await _queries.FindUserChallengeHistoryAsync(userId, game, state);

            return challenges
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}