// Filename: TokenBundles.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class TokenBundles : ReadOnlyDictionary<TokenBundleType, TokenBundle>
    {
        private static readonly Dictionary<TokenBundleType, TokenBundle> Bundles = new Dictionary<TokenBundleType, TokenBundle>
        {
            [TokenBundleType.FiftyThousand] = new TokenBundle(Money.Five, Token.FiftyThousand),
            [TokenBundleType.OneHundredThousand] = new TokenBundle(Money.Ten, Token.OneHundredThousand),
            [TokenBundleType.TwoHundredFiftyThousand] = new TokenBundle(Money.TwentyFive, Token.TwoHundredFiftyThousand),
            [TokenBundleType.FiveHundredThousand] = new TokenBundle(Money.Fifty, Token.OneMillion),
            [TokenBundleType.OneMillion] = new TokenBundle(Money.OneHundred, Token.FiveMillions),
            [TokenBundleType.FiveMillions] = new TokenBundle(Money.FiveHundred, Token.FiveMillions)
        };

        public TokenBundles() : base(Bundles)
        {
        }
    }
}