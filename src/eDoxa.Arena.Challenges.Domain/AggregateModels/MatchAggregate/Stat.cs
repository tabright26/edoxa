// Filename: Stat.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public sealed class Stat : ValueObject
    {
        public Stat(StatName name, StatValue value, StatWeighting weighting) : this()
        {
            Name = name;
            Value = value;
            Weighting = weighting;
        }

        private Stat()
        {
            // Required by EF Core.
        }

        public StatName Name { get; private set; }

        public StatValue Value { get; private set; }

        public StatWeighting Weighting { get; private set; }

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
