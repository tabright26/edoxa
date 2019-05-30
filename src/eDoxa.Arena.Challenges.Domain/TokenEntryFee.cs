// Filename: TokenEntryFee.cs
// Date Created: 2019-05-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Domain.ValueObjects;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain
{
    public sealed class TokenEntryFee : EntryFee
    {
        public static readonly TokenEntryFee TwoAndHalf = new TokenEntryFee(2500M);
        public static readonly TokenEntryFee Five = new TokenEntryFee(5000M);
        public static readonly TokenEntryFee Ten = new TokenEntryFee(10000M);
        public static readonly TokenEntryFee Twenty = new TokenEntryFee(20000M);
        public static readonly TokenEntryFee TwentyFive = new TokenEntryFee(25000M);
        public static readonly TokenEntryFee Fifty = new TokenEntryFee(50000M);
        public static readonly TokenEntryFee SeventyFive = new TokenEntryFee(75000M);
        public static readonly TokenEntryFee OneHundred = new TokenEntryFee(100000M);

        private TokenEntryFee(decimal entryFee) : base(CurrencyType.Token, entryFee)
        {
        }
    }
}
