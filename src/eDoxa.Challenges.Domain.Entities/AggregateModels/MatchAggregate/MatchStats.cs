// Filename: MatchStats.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Functional.Extensions;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.MatchAggregate
{
    public sealed class MatchStats : Dictionary<StatName, StatValue>, IMatchStats
    {
        // TODO: To defend.
        public MatchStats(LinkedMatch linkedMatch, object stats)
        {
            LinkedMatch = linkedMatch;

            stats.GetType().GetProperties().ForEach(property => this.Add(new StatName(property), new StatValue(property.GetValue(stats))));
        }

        public LinkedMatch LinkedMatch { get; }
    }
}