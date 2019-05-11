// Filename: TokenBundles.cs
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
    public sealed class TokenBundles : ReadOnlyDictionary<TokenBundleType, TokenBundle>
    {
        private static readonly Dictionary<TokenBundleType, TokenBundle> Bundles =
            new Dictionary<TokenBundleType, TokenBundle>
            {
                [TokenBundleType.FiftyThousand] = new TokenBundle(Money.Five, Token.FiftyThousand),
                [TokenBundleType.OneHundredThousand] = new TokenBundle(Money.Ten, Token.OneHundredThousand),
                [TokenBundleType.TwoHundredFiftyThousand] = new TokenBundle(Money.TwentyFive, Token.TwoHundredFiftyThousand),
                [TokenBundleType.FiveHundredThousand] = new TokenBundle(Money.Fifty, Token.FiveHundredThousand),
                [TokenBundleType.OneMillion] = new TokenBundle(Money.OneHundred, Token.OneMillion),
                [TokenBundleType.FiveMillions] = new TokenBundle(Money.FiveHundred, Token.FiveMillions)
            };

        public TokenBundles() : base(Bundles)
        {
        }

        public bool IsValid(TokenBundleType bundleType)
        {
            return this.ContainsKey(bundleType);
        }
    }
}