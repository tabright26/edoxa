// Filename: StatValue.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Globalization;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class StatValue : ValueObject
    {
        private readonly double _value;

        public StatValue(object value)
        {
            _value = Convert.ToDouble(value);
        }

        public static implicit operator double(StatValue value)
        {
            return value._value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
