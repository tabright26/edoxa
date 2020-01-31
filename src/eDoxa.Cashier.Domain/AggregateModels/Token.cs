// Filename: Token.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class Token : Currency
    {
        public static readonly Token MinValue = new Token(25000);

        public static readonly Token FiftyThousand = new Token(50000);
        public static readonly Token OneHundredThousand = new Token(100000);
        public static readonly Token TwoHundredFiftyThousand = new Token(250000);
        public static readonly Token FiveHundredThousand = new Token(500000);
        public static readonly Token OneMillion = new Token(1000000);
        public static readonly Token FiveMillions = new Token(5000000);

        public Token(decimal amount) : base(amount, CurrencyType.Token)
        {
        }

        public static Token operator -(Token token)
        {
            return new Token(-token.Amount);
        }
    }
}
