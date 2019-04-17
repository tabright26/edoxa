// Filename: TokenBundle.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class TokenBundle : CurrencyBundle<Token>
    {
        public TokenBundle(Money price, Token amount) : base(price, amount)
        {
        }

        public override Transaction CreateTransaction(User user)
        {
            return new ExternalTransaction(user, Price, TransactionDescription.TokensBought);
        }
    }
}