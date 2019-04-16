// Filename: Token.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class Token : Currency<Token>
    {
        internal static readonly Token FiftyThousand = FromDecimal(50000);
        internal static readonly Token OneHundredThousand = FromDecimal(100000);
        internal static readonly Token TwoHundredFiftyThousand = FromDecimal(250000);
        internal static readonly Token FiveHundredThousand = FromDecimal(500000);
        internal static readonly Token OneMillion = FromDecimal(1000000);
        internal static readonly Token FiveMillions = FromDecimal(5000000);

        public override string ToString()
        {
            return Convert.ToInt32(Amount).ToString();
        }
    }
}