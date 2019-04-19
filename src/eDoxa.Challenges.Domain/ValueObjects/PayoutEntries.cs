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

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public partial class PayoutEntries : ValueObject
    {
        private readonly int _payoutEntries;

        public PayoutEntries(Entries entries, PayoutRatio payoutRatio)
        {
            _payoutEntries = Convert.ToInt32(Math.Floor(entries.ToInt32() * payoutRatio.ToSingle()));
        }

        public int ToInt32()
        {
            return _payoutEntries;
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
            return _payoutEntries.CompareTo(other?._payoutEntries);
        }
    }
}