// Filename: IScoringStrategy.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Challenges.Domain.Strategies
{
    public interface IScoringStrategy
    {
        /// <summary>
        ///     Discriminator.
        /// </summary>
        Game Game { get; }

        IScoring Scoring { get; }
    }
}
