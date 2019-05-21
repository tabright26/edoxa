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
    public sealed partial class MatchExternalId
    {
        private readonly string _value;

        public MatchExternalId(string externalId)
        {
            if (string.IsNullOrWhiteSpace(externalId) ||
                !externalId.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(externalId));
            }

            _value = externalId;
        }

        public MatchExternalId(long externalId)
        {
            if (externalId < 0)
            {
                throw new ArgumentException(nameof(externalId));
            }

            _value = externalId.ToString();
        }

        public MatchExternalId(Guid externalId)
        {
            if (externalId == Guid.Empty)
            {
                throw new ArgumentException(nameof(externalId));
            }

            _value = externalId.ToString();
        }

        public static implicit operator MatchExternalId(string externalId)
        {
            return new MatchExternalId(externalId);
        }

        public static implicit operator MatchExternalId(long externalId)
        {
            return new MatchExternalId(externalId);
        }

        public static implicit operator MatchExternalId(Guid externalId)
        {
            return new MatchExternalId(externalId);
        }

        public override string ToString()
        {
            return _value;
        }
    }

    public partial class MatchExternalId : IEquatable<MatchExternalId>
    {
        public bool Equals([CanBeNull] MatchExternalId other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as MatchExternalId);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class MatchExternalId : IComparable, IComparable<MatchExternalId>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as MatchExternalId);
        }

        public int CompareTo([CanBeNull] MatchExternalId other)
        {
            return string.Compare(_value, other?._value, StringComparison.Ordinal);
        }
    }
}