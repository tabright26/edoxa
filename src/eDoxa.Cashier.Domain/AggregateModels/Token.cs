// Filename: Token.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class Token : Currency
    {
        public static readonly Token MinValue = new Token(250);
        public static readonly Token FiveHundred = new Token(500);
        public static readonly Token OneThousand = new Token(1000);
        public static readonly Token TwoThousandFiveHundred = new Token(2500);
        public static readonly Token FiveThousand = new Token(5000);
        public static readonly Token TenThousand = new Token(10000);
        public static readonly Token FiftyThousand = new Token(50000);

        public Token(decimal amount) : base(amount, CurrencyType.Token)
        {
        }

        public static Token operator -(Token token)
        {
            return new Token(-token.Amount);
        }

        public Money ToMoney()
        {
            return new Money(Amount / ConvertionRatio);
        }
    }
}
