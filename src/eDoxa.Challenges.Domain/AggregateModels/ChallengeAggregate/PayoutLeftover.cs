// Filename: PayoutLeftover.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Globalization;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class PayoutLeftover
    {
        private readonly double _value;

        public PayoutLeftover(double leftover)
        {
            _value = leftover;
        }

        public static implicit operator double(PayoutLeftover leftover)
        {
            return leftover._value;
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }

    public partial class PayoutLeftover : IEquatable<PayoutLeftover>
    {
        public bool Equals([CanBeNull] PayoutLeftover other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as PayoutLeftover);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class PayoutLeftover : IComparable, IComparable<PayoutLeftover>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as PayoutLeftover);
        }

        public int CompareTo([CanBeNull] PayoutLeftover other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}