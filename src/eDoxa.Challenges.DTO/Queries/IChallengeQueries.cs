// Filename: IChallengeQueries.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Common.Enums;
using JetBrains.Annotations;

namespace eDoxa.Challenges.DTO.Queries
{
    public interface IChallengeQueries
    {
        Task<ChallengeListDTO> FindChallengesAsync(Game game = Game.All, ChallengeType type = ChallengeType.All, ChallengeState state = ChallengeState.All);
       
        [ItemCanBeNull]
        Task<ChallengeDTO> FindChallengeAsync(ChallengeId challengeId);

        Task<ChallengeListDTO> FindUserChallengeHistoryAsync(
            UserId userId,
            Game game = Game.All,
            ChallengeType type = ChallengeType.All,
            ChallengeState state = ChallengeState.All);
    }
}