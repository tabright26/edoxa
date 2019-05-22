// Filename: EntryFeeType.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    [TypeConverter(typeof(EnumerationConverter))]
    public sealed class EntryFeeType : Enumeration<EntryFeeType>
    {
        public static readonly EntryFeeType TwoAndHalf = new EntryFeeType(1, nameof(TwoAndHalf));
        public static readonly EntryFeeType Five = new EntryFeeType(2, nameof(Five));
        public static readonly EntryFeeType Ten = new EntryFeeType(4, nameof(Ten));
        public static readonly EntryFeeType Twenty = new EntryFeeType(8, nameof(Twenty));
        public static readonly EntryFeeType TwentyFive = new EntryFeeType(10, nameof(TwentyFive));
        public static readonly EntryFeeType Fifty = new EntryFeeType(20, nameof(Fifty));
        public static readonly EntryFeeType SeventyFive = new EntryFeeType(30, nameof(SeventyFive));
        public static readonly EntryFeeType OneHundred = new EntryFeeType(40, nameof(OneHundred));

        private EntryFeeType(int value, string name) : base(value, name)
        {
        }
    }
}
