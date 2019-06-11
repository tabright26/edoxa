﻿// Filename: StatWeighting.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public sealed class StatWeighting : ValueObject
    {
        public StatWeighting(float weighting)
        {
            Value = weighting;
        }

        public float Value { get; private set; }

        public static implicit operator float(StatWeighting weighting)
        {
            return weighting.Value;
        }

        public override string ToString()
        {
            return Value.ToString("R");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
