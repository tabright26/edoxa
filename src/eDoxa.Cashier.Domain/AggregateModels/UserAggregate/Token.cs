// Filename: Token.cs
// Date Created: 2019-04-21
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
        internal static readonly Token Zero = new Token();
        internal static readonly Token FiftyThousand = new Token(50000);
        internal static readonly Token OneHundredThousand = new Token(100000);
        internal static readonly Token TwoHundredFiftyThousand = new Token(250000);
        internal static readonly Token FiveHundredThousand = new Token(500000);
        internal static readonly Token OneMillion = new Token(1000000);
        internal static readonly Token FiveMillions = new Token(5000000);

        public Token(long amount) : base(amount)
        {
        }

        private Token()
        {
        }

        public override string ToString()
        {
            return Convert.ToInt32(this).ToString();
        }
    }
}