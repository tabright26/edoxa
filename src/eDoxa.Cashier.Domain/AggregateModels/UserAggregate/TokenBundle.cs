// Filename: TokenBundle.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class TokenBundle : Bundle<Token>
    {
        public TokenBundle(Money price, Token amount) : base(price, amount)
        {
        }
    }
}