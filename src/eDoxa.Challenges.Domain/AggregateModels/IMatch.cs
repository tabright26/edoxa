// Filename: IMatch.cs
// Date Created: 2019-07-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Domain.AggregateModels
{
    public interface IMatch : IEntity<MatchId>
    {
        GameUuid GameUuid { get; }

        Score Score { get; }

        IReadOnlyCollection<Stat> Stats { get; }
    }
}
