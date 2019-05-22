// Filename: PayoutEntryType.cs
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
    public sealed class PayoutEntryType : Enumeration<PayoutEntryType>
    {
        public static readonly PayoutEntryType One = new PayoutEntryType(1, nameof(One));
        public static readonly PayoutEntryType Two = new PayoutEntryType(2, nameof(Two));
        public static readonly PayoutEntryType Three = new PayoutEntryType(3, nameof(Three));
        public static readonly PayoutEntryType Four = new PayoutEntryType(4, nameof(Four));
        public static readonly PayoutEntryType Five = new PayoutEntryType(5, nameof(Five));
        public static readonly PayoutEntryType Ten = new PayoutEntryType(10, nameof(Ten));
        public static readonly PayoutEntryType Fifteen = new PayoutEntryType(15, nameof(Fifteen));
        public static readonly PayoutEntryType Twenty = new PayoutEntryType(20, nameof(Twenty));
        public static readonly PayoutEntryType TwentyFive = new PayoutEntryType(25, nameof(TwentyFive));
        public static readonly PayoutEntryType Fifty = new PayoutEntryType(50, nameof(Fifty));
        public static readonly PayoutEntryType SeventyFive = new PayoutEntryType(75, nameof(SeventyFive));
        public static readonly PayoutEntryType OneHundred = new PayoutEntryType(100, nameof(OneHundred));

        private PayoutEntryType(int value, string name) : base(value, name)
        {
        }
    }
}
