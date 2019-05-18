// Filename: TokenDepositBundleType.cs
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
    public sealed class TokenDepositBundleType : Enumeration<TokenDepositBundleType>
    {
        public static readonly TokenDepositBundleType FiftyThousand = new TokenDepositBundleType(1 << 0, nameof(FiftyThousand));
        public static readonly TokenDepositBundleType OneHundredThousand = new TokenDepositBundleType(1 << 1, nameof(OneHundredThousand));
        public static readonly TokenDepositBundleType TwoHundredFiftyThousand = new TokenDepositBundleType(1 << 2, nameof(TwoHundredFiftyThousand));
        public static readonly TokenDepositBundleType FiveHundredThousand = new TokenDepositBundleType(1 << 3, nameof(FiveHundredThousand));
        public static readonly TokenDepositBundleType OneMillion = new TokenDepositBundleType(1 << 4, nameof(OneMillion));
        public static readonly TokenDepositBundleType FiveMillions = new TokenDepositBundleType(1 << 5, nameof(FiveMillions));

        private TokenDepositBundleType(int value, string name) : base(value, name)
        {
        }
    }
}