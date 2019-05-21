// Filename: IChallengeRepository.cs
// Date Created: 2019-04-21
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
        Task<IReadOnlyCollection<Challenge>> FindChallengesAsync(Game game, ChallengeState state);

        [ItemCanBeNull]
        Task<Challenge> FindChallengeAsync(ChallengeId challengeId);
    }
}