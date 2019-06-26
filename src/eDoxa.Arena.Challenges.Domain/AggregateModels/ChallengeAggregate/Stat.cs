// Filename: Stat.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Stat : ValueObject
    {
        public Stat(StatName name, StatValue value, StatWeighting weighting)
        {
            Name = name;
            Value = value;
            Weighting = weighting;
        }

        public StatName Name { get; }

        public StatValue Value { get; }

        public StatWeighting Weighting { get; }

        public Score Score => new StatScore(this);

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Value;
            yield return Weighting;
        }

        public override string ToString()
        {
            return Score.ToString();
        }
    }
}
