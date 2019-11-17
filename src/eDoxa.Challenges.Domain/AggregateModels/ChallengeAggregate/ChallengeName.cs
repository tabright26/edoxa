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

using eDoxa.Seedwork.Domain;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeName : ValueObject
    {
        private readonly string _name;

        public ChallengeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || !name.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '(' || c == ')'))
            {
                throw new ArgumentException(nameof(name));
            }

            _name = name;
        }

        public static implicit operator string(ChallengeName name)
        {
            return name._name;
        }

        public override string ToString()
        {
            return _name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _name;
        }
    }
}
