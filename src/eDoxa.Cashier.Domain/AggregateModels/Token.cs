// Filename: Token.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Globalization;

using eDoxa.Seedwork.Domain.Aggregate;

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
