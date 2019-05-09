// Filename: IChallengeQueries.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Functional.Maybe;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Challenges.DTO.Queries
{
    public interface IChallengeQueries
    {
        Task<Option<ChallengeListDTO>> FindChallengesAsync(Game game, ChallengeType type, ChallengeState state);

        Task<Option<ChallengeDTO>> FindChallengeAsync(ChallengeId challengeId);

        Task<Option<ChallengeListDTO>> FindUserChallengeHistoryAsync(UserId userId, Game game, ChallengeType type, ChallengeState state);
    }
}