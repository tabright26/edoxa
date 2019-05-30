// Filename: Token.cs
// Date Created: 2019-05-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Globalization;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class Token : Currency
    {
        public static readonly Token FiftyThousand = new Token(50000);
        public static readonly Token OneHundredThousand = new Token(100000);
        public static readonly Token TwoHundredFiftyThousand = new Token(250000);
        public static readonly Token FiveHundredThousand = new Token(500000);
        public static readonly Token OneMillion = new Token(1000000);
        public static readonly Token FiveMillions = new Token(5000000);

        public Token(long amount) : base(CurrencyType.Token, amount)
        {
        }

        public static Token operator -(Token token)
        {
            return new Token(-Convert.ToInt64(token.Amount));
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}
