﻿// Filename: IChallengeRepository.cs
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
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Repositories
{
    public interface IChallengeRepository : IRepository<Challenge>
    {
        void Create(Challenge challenge);

        Task<IReadOnlyCollection<Challenge>> FindChallengesAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null);

        [ItemCanBeNull]
        Task<Challenge> FindChallengeAsync(ChallengeId challengeId);

        Task<IReadOnlyCollection<Challenge>> FindUserChallengeHistoryAsNoTrackingAsync(
            UserId userId,
            [CanBeNull] Game game = null,
            [CanBeNull] ChallengeState state = null
        );

        Task<IReadOnlyCollection<Challenge>> FindChallengesAsNoTrackingAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null);

        Task<Challenge> FindChallengeAsNoTrackingAsync(ChallengeId challengeId);
    }
}
