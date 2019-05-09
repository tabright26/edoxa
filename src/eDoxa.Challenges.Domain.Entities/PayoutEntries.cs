// Filename: PayoutEntries.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Challenges.Domain.Entities.Abstractions;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.Entities
{
    public partial class PayoutEntries
    {
        private readonly int _value;

        public PayoutEntries(Entries entries, PayoutRatio payoutRatio)
        {
            _value = Convert.ToInt32(Math.Floor(entries * payoutRatio));
        }

        public PayoutEntries(IPayout payout)
        {
            _value = payout.Buckets.Sum(bucket => bucket.Size);
        }

        public static implicit operator int(PayoutEntries payoutEntries)
        {
            return payoutEntries._value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }

    public partial class PayoutEntries : IEquatable<PayoutEntries>
    {
        public bool Equals(PayoutEntries other)
        {
            return _value.Equals(other?._value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PayoutEntries);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }

    public partial class PayoutEntries : IComparable, IComparable<PayoutEntries>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as PayoutEntries);
        }

        public int CompareTo([CanBeNull] PayoutEntries other)
        {
            return _value.CompareTo(other?._value);
        }
    }
}