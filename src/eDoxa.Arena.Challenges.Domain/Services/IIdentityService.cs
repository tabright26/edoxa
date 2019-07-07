﻿// Filename: IIdentityService.cs
// Date Created: 2019-07-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Services
{
    public interface IIdentityService
    {
        Task<bool> HasGameAccountIdAsync(UserId userId, ChallengeGame game);

        [ItemCanBeNull]
        Task<GameAccountId> GetGameAccountIdAsync(UserId userId, ChallengeGame game);
    }
}