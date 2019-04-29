// Filename: StatValue.cs
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

namespace eDoxa.Challenges.Domain.AggregateModels.MatchAggregate
{
    public partial class StatValue
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
    }

    public partial class StatValue : IEquatable<StatValue>
    {
        public bool Equals([CanBeNull] StatValue other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as StatValue);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class StatValue : IComparable, IComparable<StatValue>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as StatValue);
        }

        public int CompareTo([CanBeNull] StatValue other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}