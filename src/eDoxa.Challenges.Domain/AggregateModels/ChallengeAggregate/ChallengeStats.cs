// Filename: ChallengeStats.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    internal sealed class ChallengeStats : Dictionary<string, object>, IChallengeStats
    {
        internal ChallengeStats(LinkedMatch linkedMatch, object stats)
        {
            LinkedMatch = linkedMatch;

            foreach (var property in stats.GetType().GetProperties())
            {
                var name = property.GetMethod.Name.Substring(4);

                var value = property.GetValue(stats);

                this.Add(name, value);
            }
        }

        public LinkedMatch LinkedMatch { get; }
    }
}