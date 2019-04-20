// Filename: ChallengeStats.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    internal sealed class ChallengeStats : Dictionary<StatName, StatValue>, IChallengeStats
    {
        internal ChallengeStats(LinkedMatch linkedMatch, object stats)
        {
            LinkedMatch = linkedMatch;

            foreach (var property in stats.GetType().GetProperties())
            {
                var name = property.GetMethod.Name.Substring(4);

                var value = new StatValue(property.GetValue(stats));

                this.Add(name, value);
            }
        }

        public LinkedMatch LinkedMatch { get; }
    }
}