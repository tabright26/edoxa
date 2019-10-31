// Filename: IGameReferencesAdapter.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Challenges.Domain.Adapters
{
    public interface IGameReferencesAdapter
    {
        /// <summary>
        ///     Discriminator.
        /// </summary>
        Game Game { get; }

        Task<IEnumerable<GameReference>> GetGameReferencesAsync(GameAccountId gameAccountId, DateTime startedAt, DateTime endedAt);
    }
}
