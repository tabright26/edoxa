// Filename: TokenDepositBundles.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Collections.ObjectModel;

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate
{
    public sealed class TokenDepositBundles : ReadOnlyDictionary<TokenDepositBundleType, TokenBundle>
    {
        private static readonly Dictionary<TokenDepositBundleType, TokenBundle> Bundles =
            new Dictionary<TokenDepositBundleType, TokenBundle>
            {
                [TokenDepositBundleType.FiftyThousand] = new TokenBundle(Money.Five, Token.FiftyThousand),
                [TokenDepositBundleType.OneHundredThousand] = new TokenBundle(Money.Ten, Token.OneHundredThousand),
                [TokenDepositBundleType.TwoHundredFiftyThousand] = new TokenBundle(Money.TwentyFive, Token.TwoHundredFiftyThousand),
                [TokenDepositBundleType.FiveHundredThousand] = new TokenBundle(Money.Fifty, Token.FiveHundredThousand),
                [TokenDepositBundleType.OneMillion] = new TokenBundle(Money.OneHundred, Token.OneMillion),
                [TokenDepositBundleType.FiveMillions] = new TokenBundle(Money.FiveHundred, Token.FiveMillions)
            };

        public TokenDepositBundles() : base(Bundles)
        {
        }

        public bool IsValid(TokenDepositBundleType bundleType)
        {
            return this.ContainsKey(bundleType);
        }
    }
}