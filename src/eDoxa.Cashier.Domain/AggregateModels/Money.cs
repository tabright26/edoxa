// Filename: Money.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public class Money : Currency
    {
        public static readonly Money MinValue = new Money(2.5M);

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

        public static Money operator -(Money money)
        {
            return new Money(-money.Amount);
        }

        public Token ToToken()
        {
            return new Token(Amount * ConvertionRatio);
        }
    }
}
