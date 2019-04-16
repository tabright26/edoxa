// Filename: Money.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class Money : Currency<Money>
    {
        internal static readonly Money Five = FromDecimal(5);
        internal static readonly Money Ten = FromDecimal(10);
        internal static readonly Money Twenty = FromDecimal(20);
        internal static readonly Money TwentyFive = FromDecimal(25);
        internal static readonly Money Fifty = FromDecimal(50);
        internal static readonly Money OneHundred = FromDecimal(100);
        internal static readonly Money FiveHundred = FromDecimal(500);

        public override string ToString()
        {
            return $"${Amount:0.00}";
        }

        public int AsCents()
        {
            return Convert.ToInt32(Amount * 100);
        }
    }
}