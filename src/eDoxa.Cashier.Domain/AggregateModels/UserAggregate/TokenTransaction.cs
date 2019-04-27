// Filename: TokenTransaction.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Functional.Maybe;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class TokenTransaction : Transaction<Token>, ITokenTransaction
    {
        public TokenTransaction(Token amount) : base(amount)
        {
        }

        protected TokenTransaction(Token amount, ActivityId activityId) : base(amount, activityId.ToString())
        {
        }

        public Option<TokenTransaction> TryPayoff(Token amount)
        {
            this.Complete();

            return -Amount < amount ? new Option<TokenTransaction>(new TokenTransaction(amount)) : new Option<TokenTransaction>();
        }
    }
}