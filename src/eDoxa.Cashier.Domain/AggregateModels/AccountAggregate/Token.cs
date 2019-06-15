// Filename: Token.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Globalization;

using eDoxa.Seedwork.Common.Attributes;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class Token : Currency
    {
        [AllowValue(true)] public static readonly Token FiftyThousand = new Token(50000);
        [AllowValue(true)] public static readonly Token OneHundredThousand = new Token(100000);
        [AllowValue(true)] public static readonly Token TwoHundredFiftyThousand = new Token(250000);
        [AllowValue(true)] public static readonly Token FiveHundredThousand = new Token(500000);
        [AllowValue(true)] public static readonly Token OneMillion = new Token(1000000);
        [AllowValue(false)] public static readonly Token FiveMillions = new Token(5000000);

        public Token(decimal amount) : base(amount, CurrencyType.Token)
        {
        }

        public static Token operator -(Token token)
        {
            return new Token(-System.Convert.ToInt64(token.Amount));
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}
