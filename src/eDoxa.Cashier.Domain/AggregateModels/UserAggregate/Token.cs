// Filename: Token.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Globalization;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class Token : Currency<Token>
    {
        public static readonly Token FiftyThousand = FromDecimal(50000);
        public static readonly Token OneHundredThousand = FromDecimal(100000);
        public static readonly Token TwoHundredFiftyThousand = FromDecimal(250000);
        public static readonly Token FiveHundredThousand = FromDecimal(500000);
        public static readonly Token OneMillion = FromDecimal(1000000);
        public static readonly Token FiveMillions = FromDecimal(5000000);

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}