﻿// Filename: IMatchStatsAdapter.cs
// Date Created: 2019-06-25
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

namespace eDoxa.Arena.Challenges.Domain.Adapters
{
    public interface IMatchStatsAdapter
    {
        /// <summary>
        ///     Discriminator.
        /// </summary>
        ChallengeGame Game { get; }

        Task<IMatchStats> GetMatchStatsAsync(GameAccountId gameAccountId, GameReference gameReference);
    }
}
