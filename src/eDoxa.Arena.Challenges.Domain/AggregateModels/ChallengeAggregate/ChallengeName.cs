// Filename: ChallengeName.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class ChallengeName
    {
        private readonly string _value;

        public ChallengeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                !name.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '(' || c == ')'))
            {
                throw new ArgumentException(nameof(name));
            }

            _value = name.Trim();
        }

        public override string ToString()
        {
            return _value;
        }
    }

    public partial class ChallengeName : IEquatable<ChallengeName>
    {
        public bool Equals(ChallengeName other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ChallengeName);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class ChallengeName : IComparable, IComparable<ChallengeName>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as ChallengeName);
        }

        public int CompareTo([CanBeNull] ChallengeName other)
        {
            return string.Compare(_value, other?._value, StringComparison.Ordinal);
        }
    }
}