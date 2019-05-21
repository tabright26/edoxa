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
        private readonly IChallengeQuery _query;
        private readonly IUserInfoService _userInfoService;

        public ChallengeHistoryController(IUserInfoService userInfoService, IChallengeQuery query)
        {
            _userInfoService = userInfoService;
            _query = query;
        }

        /// <summary>
        ///     Find the challenge history of a user.
        /// </summary>
        [HttpGet(Name = nameof(FindUserChallengeHistoryAsync))]
        public async Task<IActionResult> FindUserChallengeHistoryAsync([CanBeNull] Game game)
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            game = game ?? Game.All;

            var challenges = await _query.FindUserChallengeHistoryAsync(userId, game);

            return challenges
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}