// Filename: IMatchReferencesFactory.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Abstractions.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Domain.Abstractions.Factories
{
    public interface IGameMatchIdsFactory
    {
        Task<IGameMatchIdsAdapter> CreateAdapterAsync(ChallengeGame game, GameAccountId gameAccountId, ChallengeTimeline timeline);
    }
}
