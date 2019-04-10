// Filename: Money.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class Money : Currency<Money>
    {
        public static readonly Money Ten = FromDecimal(10);
        public static readonly Money Twenty = FromDecimal(20);
        public static readonly Money Fifty = FromDecimal(50);
        public static readonly Money OneHundred = FromDecimal(100);
        public static readonly Money FiveHundred = FromDecimal(500);

        public override string ToString()
        {
            return $"${Amount:0.00}";
        }
    }
}