// Filename: TokenBundleType.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate
{
    [TypeConverter(typeof(EnumerationConverter))]
    public sealed class TokenBundleType : Enumeration<TokenBundleType>
    {
        public static readonly TokenBundleType FiftyThousand = new TokenBundleType(1 << 0, nameof(FiftyThousand));
        public static readonly TokenBundleType OneHundredThousand = new TokenBundleType(1 << 1, nameof(OneHundredThousand));
        public static readonly TokenBundleType TwoHundredFiftyThousand = new TokenBundleType(1 << 2, nameof(TwoHundredFiftyThousand));
        public static readonly TokenBundleType FiveHundredThousand = new TokenBundleType(1 << 3, nameof(FiveHundredThousand));
        public static readonly TokenBundleType OneMillion = new TokenBundleType(1 << 4, nameof(OneMillion));
        public static readonly TokenBundleType FiveMillions = new TokenBundleType(1 << 5, nameof(FiveMillions));

        private TokenBundleType(int value, string name) : base(value, name)
        {
        }
    }
}