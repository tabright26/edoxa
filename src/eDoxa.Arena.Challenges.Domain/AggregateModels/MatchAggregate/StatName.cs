// Filename: StatName.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Reflection;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public class StatName : ValueObject
    {
        public StatName(PropertyInfo propertyInfo) : this()
        {
            Value = propertyInfo.GetMethod.Name.Substring(4);
        }

        public StatName(string name) : this()
        {
            Value = name;
        }

        private StatName()
        {
            // Required by EF Core.
        }

        public string Value { get; private set; }

        public static implicit operator StatName(string name)
        {
            return new StatName(name);
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
