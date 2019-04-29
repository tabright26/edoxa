﻿// Filename: IChallengeQueries.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Functional.Maybe;
using eDoxa.Seedwork.Domain.Common.Enums;

namespace eDoxa.Challenges.DTO.Queries
{
    public interface IChallengeQueries
    {
        Task<Option<ChallengeListDTO>> FindChallengesAsync(
            Game game = Game.All,
            ChallengeType type = ChallengeType.All,
            ChallengeState1 state = ChallengeState1.All);

        Task<Option<ChallengeDTO>> FindChallengeAsync(ChallengeId challengeId);

        Task<Option<ChallengeListDTO>> FindUserChallengeHistoryAsync(
            UserId userId,
            Game game = Game.All,
            ChallengeType type = ChallengeType.All,
            ChallengeState1 state = ChallengeState1.All);
    }
}