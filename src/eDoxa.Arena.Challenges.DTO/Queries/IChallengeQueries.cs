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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Challenges.DTO.Queries
{
    public interface IChallengeQueries
    {
        Task<Option<ChallengeListDTO>> FindChallengesAsync(Game game, ChallengeState state);

        Task<Option<ChallengeDTO>> FindChallengeAsync(ChallengeId challengeId);

        Task<Option<ChallengeListDTO>> FindUserChallengeHistoryAsync(UserId userId, Game game, ChallengeState state);
    }
}