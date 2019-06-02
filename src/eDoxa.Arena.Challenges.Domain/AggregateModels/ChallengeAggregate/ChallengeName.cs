// Filename: ChallengeName.cs
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
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeName : ValueObject
    {
        public ChallengeName(string name) : this()
        {
            if (string.IsNullOrWhiteSpace(name) || !name.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '(' || c == ')'))
            {
                throw new ArgumentException(nameof(name));
            }

            Value = name;
        }

        private ChallengeName()
        {
            // Required by EF Core.   
        }

        public string Value { get; private set; }

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
