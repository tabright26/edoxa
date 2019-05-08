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

using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Security.Abstractions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Enumerations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/challenges/history")]
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
        public async Task<IActionResult> FindUserChallengeHistoryAsync(string type, string game, string state)
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            var challenges = await _queries.FindUserChallengeHistoryAsync(userId, Enumeration.FromAnyDisplayName<ChallengeType>(type), Enumeration.FromAnyDisplayName<Game>(game), Enumeration.FromAnyDisplayName<ChallengeState>(state));

            return challenges
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}