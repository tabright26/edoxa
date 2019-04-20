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

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels
{
    public partial class PayoutEntries : ValueObject
    {
        private readonly int _value;

        public PayoutEntries(Entries entries, PayoutRatio payoutRatio)
        {
            _value = Convert.ToInt32(Math.Floor(entries * payoutRatio));
        }

        public static implicit operator int(PayoutEntries payoutEntries)
        {
            return payoutEntries._value;
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