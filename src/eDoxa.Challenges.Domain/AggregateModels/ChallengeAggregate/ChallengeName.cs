// Filename: ChallengeName.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeName : ValueObject
    {
        private string _value;

        public ChallengeName(string value)
        {
            Value = value;
        }

        public string Value
        {
            get
            {
                return _value;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Value));
                }

                if (!value.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '(' || c == ')'))
                {
                    throw new FormatException(nameof(Value));
                }

                _value = value.Trim();
            }
        }

        public static implicit operator string(ChallengeName name)
        {
            return name.ToString();
        }

        public static implicit operator ChallengeName(string name)
        {
            return new ChallengeName(name);
        }

        public override string ToString()
        {
            return _value;
        }
    }
}