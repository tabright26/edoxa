// Filename: Token.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate
{
    public sealed class Token : TypeObject<Token, long>, ICurrency
    {
        public static readonly Token FiftyThousand = new Token(50000);
        public static readonly Token OneHundredThousand = new Token(100000);
        public static readonly Token TwoHundredFiftyThousand = new Token(250000);
        public static readonly Token FiveHundredThousand = new Token(500000);
        public static readonly Token OneMillion = new Token(1000000);
        public static readonly Token FiveMillions = new Token(5000000);

        public Token(long amount) : base(amount)
        {
        }

        public static implicit operator decimal(Token token)
        {
            return token.Value;
        }

        public static Token operator -(Token token)
        {
            return new Token(-token.Value);
        }
    }
}
