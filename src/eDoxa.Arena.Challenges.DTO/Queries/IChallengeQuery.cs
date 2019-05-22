﻿// Filename: IChallengeQuery.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Challenges.DTO.Queries
{
    public interface IChallengeQuery
    {
        Task<Option<ChallengeListDTO>> FindChallengesAsync(Game game);

        Task<Option<ChallengeDTO>> FindChallengeAsync(ChallengeId challengeId);

        Task<Option<ChallengeListDTO>> FindUserChallengeHistoryAsync(UserId userId, Game game);
    }
}