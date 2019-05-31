// Filename: Money.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class Money : Currency
    {
        public static readonly Money Five = new Money(5);
        public static readonly Money Ten = new Money(10);
        public static readonly Money Twenty = new Money(20);
        public static readonly Money TwentyFive = new Money(25);
        public static readonly Money Fifty = new Money(50);
        public static readonly Money OneHundred = new Money(100);
        public static readonly Money TwoHundred = new Money(200);
        public static readonly Money FiveHundred = new Money(500);

        public Money(decimal amount) : base(amount, CurrencyType.Money)
        {
        }

        public static Money operator -(Money token)
        {
            return new Money(-token.Amount);
        }

        public override string ToString()
        {
            return Amount.ToString("$##.##");
        }

        public long ToCents()
        {
            return System.Convert.ToInt64(Amount * 100);
        }
    }
}
