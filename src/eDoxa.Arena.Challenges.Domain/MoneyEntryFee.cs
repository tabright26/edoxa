// Filename: MoneyEntryFee.cs
// Date Created: 2019-05-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Domain;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class MoneyEntryFee : EntryFee
    {
        public static readonly MoneyEntryFee TwoAndHalf = new MoneyEntryFee(2.5M);
        public static readonly MoneyEntryFee Five = new MoneyEntryFee(5M);
        public static readonly MoneyEntryFee Ten = new MoneyEntryFee(10M);
        public static readonly MoneyEntryFee Twenty = new MoneyEntryFee(20M);
        public static readonly MoneyEntryFee TwentyFive = new MoneyEntryFee(25M);
        public static readonly MoneyEntryFee Fifty = new MoneyEntryFee(50M);
        public static readonly MoneyEntryFee SeventyFive = new MoneyEntryFee(75M);
        public static readonly MoneyEntryFee OneHundred = new MoneyEntryFee(100M);

        private MoneyEntryFee(decimal entryFee) : base(entryFee, Currency.Money)
        {
        }
    }
}
