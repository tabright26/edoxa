// Filename: StatName.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class StatName
    {
        private readonly string _value;

        public StatName(string name)
        {
            _value = name;
        }

        public static implicit operator StatName(string name)
        {
            return new StatName(name);
        }

        public override string ToString()
        {
            return _value;
        }
    }

    public partial class StatName : IEquatable<StatName>
    {
        public bool Equals([CanBeNull] StatName other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as StatName);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class StatName : IComparable, IComparable<StatName>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as StatName);
        }

        public int CompareTo([CanBeNull] StatName other)
        {
            return string.Compare(_value, other?._value, StringComparison.Ordinal);
        }
    }
}