// Filename: Token.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class Token : ValueObject, ICurrency
    {
        public static readonly Token FiftyThousand = new Token(50000);
        public static readonly Token OneHundredThousand = new Token(100000);
        public static readonly Token TwoHundredFiftyThousand = new Token(250000);
        public static readonly Token FiveHundredThousand = new Token(500000);
        public static readonly Token OneMillion = new Token(1000000);
        public static readonly Token FiveMillions = new Token(5000000);

        public Token(decimal amount)
        {
            Type = Currency.Token;
            Amount = amount;
        }

        public Currency Type { get; }

        public decimal Amount { get; }

        public static implicit operator decimal(Token money)
        {
            return money.Amount;
        }

        public static Token operator -(Token token)
        {
            return new Token(-token.Amount);
        }

        public static IImmutableSet<Token> DepositAmounts()
        {
            return new[]
            {
                FiftyThousand,
                OneHundredThousand,
                TwoHundredFiftyThousand,
                FiveHundredThousand,
                OneMillion,
                FiveMillions
            }.ToImmutableHashSet();
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
            yield return Amount;
        }
    }
}
