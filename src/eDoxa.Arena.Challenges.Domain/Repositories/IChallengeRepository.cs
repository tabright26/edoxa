// Filename: IChallengeRepository.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Repositories
{
    public interface IChallengeRepository : IRepository<Challenge>
    {
        void Create(Challenge challenge);

        Task<IReadOnlyCollection<Challenge>> FindChallengesAsync(Game game);

        [ItemCanBeNull]
        Task<Challenge> FindChallengeAsync(ChallengeId challengeId);

        Task<IReadOnlyCollection<Challenge>> FindUserChallengeHistoryAsNoTrackingAsync(UserId userId, Game game);

        Task<IReadOnlyCollection<Challenge>> FindChallengesAsNoTrackingAsync(Game game);

        Task<Challenge> FindChallengeAsNoTrackingAsync(ChallengeId challengeId);
    }
}
