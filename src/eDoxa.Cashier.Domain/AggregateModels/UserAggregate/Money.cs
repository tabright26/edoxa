// Filename: Money.cs
// Date Created: 2019-04-21
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
        internal static readonly Money Zero = new Money();
        internal static readonly Money Five = new Money(5);
        internal static readonly Money Ten = new Money(10);
        internal static readonly Money Twenty = new Money(20);
        internal static readonly Money TwentyFive = new Money(25);
        internal static readonly Money Fifty = new Money(50);
        internal static readonly Money OneHundred = new Money(100);
        internal static readonly Money FiveHundred = new Money(500);

        public Money(decimal amount) : base(amount)
        {
        }

        private Money()
        {
        }

        public override string ToString()
        {
            return Convert.ToDecimal(this).ToString("C");
        }

        public int AsCents()
        {
            return Convert.ToInt32(this * 100);
        }
    }
}