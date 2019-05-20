// Filename: LinkedMatch.cs
// Date Created: 2019-04-21
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

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public sealed partial class LinkedMatch
    {
        private readonly string _value;

        public LinkedMatch(string linkedMatch)
        {
            if (string.IsNullOrWhiteSpace(linkedMatch) ||
                !linkedMatch.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(linkedMatch));
            }

            _value = linkedMatch;
        }

        public LinkedMatch(long linkedMatch)
        {
            if (linkedMatch < 0)
            {
                throw new ArgumentException(nameof(linkedMatch));
            }

            _value = linkedMatch.ToString();
        }

        public LinkedMatch(Guid linkedMatch)
        {
            if (linkedMatch == Guid.Empty)
            {
                throw new ArgumentException(nameof(linkedMatch));
            }

            _value = linkedMatch.ToString();
        }

        public static implicit operator LinkedMatch(string linkedMatch)
        {
            return new LinkedMatch(linkedMatch);
        }

        public static implicit operator LinkedMatch(long linkedMatch)
        {
            return new LinkedMatch(linkedMatch);
        }

        public static implicit operator LinkedMatch(Guid linkedMatch)
        {
            return new LinkedMatch(linkedMatch);
        }

        public override string ToString()
        {
            return _value;
        }
    }

    public partial class LinkedMatch : IEquatable<LinkedMatch>
    {
        public bool Equals(LinkedMatch other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as LinkedMatch);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class LinkedMatch : IComparable, IComparable<LinkedMatch>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as LinkedMatch);
        }

        public int CompareTo([CanBeNull] LinkedMatch other)
        {
            return string.Compare(_value, other?._value, StringComparison.Ordinal);
        }
    }
}