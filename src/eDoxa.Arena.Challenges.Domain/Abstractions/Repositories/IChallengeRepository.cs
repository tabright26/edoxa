// Filename: IChallengeRepository.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Specifications.Abstractions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Abstractions.Repositories
{
    public interface IChallengeRepository : IRepository<Challenge>
    {
        void Create(Challenge challenge);

        Task<IReadOnlyCollection<Challenge>> FindChallengesAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null);

        Task<IReadOnlyCollection<Challenge>> FindChallengesAsync(ISpecification<Challenge> specification);

        Task<Challenge> FindChallengeAsync(ChallengeId challengeId);

        Task<IReadOnlyCollection<Challenge>> FindUserChallengeHistoryAsNoTrackingAsync(
            UserId userId,
            [CanBeNull] Game game = null,
            [CanBeNull] ChallengeState state = null
        );

        Task<IReadOnlyCollection<Challenge>> FindChallengesAsNoTrackingAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null);

        [ItemCanBeNull]
        Task<Challenge> FindChallengeAsNoTrackingAsync(ChallengeId challengeId);
    }
}
