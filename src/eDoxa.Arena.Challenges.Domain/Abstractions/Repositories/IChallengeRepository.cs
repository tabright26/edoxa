﻿// Filename: IChallengeRepository.cs
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
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Specifications.Abstractions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Abstractions.Repositories
{
    public interface IChallengeRepository : IRepository<Challenge>
    {
        void Create(Challenge challenge);

        void Create(IEnumerable<Challenge> challenges);

        Task<IReadOnlyCollection<Challenge>> FindChallengesAsync(Game game = null, ChallengeState state = null);

        Task<IReadOnlyCollection<Challenge>> FindChallengesAsync(ISpecification<Challenge> specification);

        Task<Challenge> FindChallengeAsync(ChallengeId challengeId);

        Task<IReadOnlyCollection<Challenge>> FindUserChallengeHistoryAsNoTrackingAsync(UserId userId, Game game = null, ChallengeState state = null);

        Task<IReadOnlyCollection<Challenge>> FindChallengesAsNoTrackingAsync(Game game = null, ChallengeState state = null);

        [ItemCanBeNull]
        Task<Challenge> FindChallengeAsNoTrackingAsync(ChallengeId challengeId);

        Task<bool> ChallengeSeedExistsAsync(int seed);
    }
}
