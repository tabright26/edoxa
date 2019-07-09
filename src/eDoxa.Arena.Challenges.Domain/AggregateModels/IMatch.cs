// Filename: IMatch.cs
// Date Created: 2019-07-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels
{
    public interface IMatch : IEntity<MatchId>
    {
        DateTime SynchronizedAt { get; }

        GameReference GameReference { get; }

        Score TotalScore { get; }

        IReadOnlyCollection<Stat> Stats { get; }
    }
}
