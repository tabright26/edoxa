// Filename: IChallengeRepository.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Common.Enums;

namespace eDoxa.Challenges.Domain.Repositories
{
    public interface IChallengeRepository : IRepository<Challenge>
    {
        void Create(IEnumerable<Challenge> challenges);

        Task<IReadOnlyCollection<Challenge>> FindChallengesAsync(
            Game game = Game.All,
            ChallengeType type = ChallengeType.All,
            ChallengeState1 state = ChallengeState1.All);

        Task<Challenge> FindChallengeAsync(ChallengeId challengeId);
    }
}