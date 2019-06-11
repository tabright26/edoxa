// Filename: ChallengeStat.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ScoringItem : ValueObject
    {
        public ScoringItem(StatName name, StatWeighting weighting) : this()
        {
            Name = name;
            Weighting = weighting;
        }

        private ScoringItem()
        {
            // Required by EF Core.
        }

        public StatName Name { get; private set; }

        public StatWeighting Weighting { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Weighting;
        }

        public override string ToString()
        {
            return $"{Name}={Weighting}";
        }
    }
}
