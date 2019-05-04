﻿// Filename: UsersController.cs
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

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Security.Abstractions;
using eDoxa.Seedwork.Domain.Common.Enums;

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
        private readonly IUserProfile _userProfile;
        private readonly IChallengeQueries _queries;

        public ChallengeHistoryController(IUserProfile userProfile, IChallengeQueries queries)
        {
            _userProfile = userProfile;
            _queries = queries;
        }

        /// <summary>
        ///     Find the challenge history of a user.
        /// </summary>
        [HttpGet(Name = nameof(FindUserChallengeHistoryAsync))]
        public async Task<IActionResult> FindUserChallengeHistoryAsync(Game game = Game.All, ChallengeType type = ChallengeType.All, ChallengeState1 state = ChallengeState1.All)
        {
            var userId = UserId.Parse(_userProfile.Subject);

            var challenges = await _queries.FindUserChallengeHistoryAsync(userId, game, type, state);

            return challenges
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}