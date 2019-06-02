// Filename: StatValue.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public class StatValue : ValueObject
    {
        public StatValue(object value) : this()
        {
            Value = Convert.ToDouble(value);
        }

        private StatValue()
        {
            // Required by EF Core.
        }

        public double Value { get; private set; }

        public static implicit operator double(StatValue statValue)
        {
            return statValue.Value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
