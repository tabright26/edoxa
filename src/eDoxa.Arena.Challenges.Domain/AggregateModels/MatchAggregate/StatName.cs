// Filename: StatName.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Reflection;

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public sealed class StatName : ValueObject, IComparable
    {
        public StatName(PropertyInfo propertyInfo)
        {
            Value = propertyInfo.GetMethod.Name.Substring(4);
        }

        public StatName(string name)
        {
            Value = name;
        }

        public string Value { get; private set; }

        public int CompareTo([CanBeNull] object obj)
        {
            return string.Compare(Value, ((StatName) obj)?.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
