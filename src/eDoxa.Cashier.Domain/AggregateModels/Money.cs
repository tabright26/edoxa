// Filename: Money.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class Money : ValueObject, ICurrency
    {
        public static readonly Money Five = new Money(5);
        public static readonly Money Ten = new Money(10);
        public static readonly Money Twenty = new Money(20);
        public static readonly Money TwentyFive = new Money(25);
        public static readonly Money Fifty = new Money(50);
        public static readonly Money OneHundred = new Money(100);
        public static readonly Money TwoHundred = new Money(200);
        public static readonly Money FiveHundred = new Money(500);

        public Money(decimal amount)
        {
            Type = Currency.Money;
            Amount = amount;
        }

        public Currency Type { get; }

        public decimal Amount { get; }

        public static implicit operator decimal(Money money)
        {
            return money.Amount;
        }

        public static Money operator -(Money money)
        {
            return new Money(-money.Amount);
        }

        public static IImmutableSet<Money> DepositAmounts()
        {
            return new[]
            {
                Ten,
                Twenty,
                Fifty,
                OneHundred,
                FiveHundred
            }.ToImmutableHashSet();
        }

        public static IImmutableSet<Money> WithdrawalAmounts()
        {
            return new[] {Fifty, OneHundred, TwoHundred}.ToImmutableHashSet();
        }

        public override string ToString()
        {
            return Amount.ToString("$##.##");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
            yield return Amount;
        }

        public long ToCents()
        {
            return Convert.ToInt64(Amount * 100);
        }
    }
}
