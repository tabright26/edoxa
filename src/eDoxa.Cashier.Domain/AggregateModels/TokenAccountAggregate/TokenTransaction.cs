// Filename: TokenTransaction.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Functional.Maybe;

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate
{
    public class TokenTransaction : Transaction<Token>, ITokenTransaction
    {
        public TokenTransaction(Token amount) : base(amount)
        {
        }

        protected TokenTransaction(Token amount, ServiceId serviceId) : base(amount, serviceId.ToString())
        {
        }

        public Option<TokenTransaction> TryPayoff(Token amount)
        {
            this.Complete();

            return -Amount < amount ? new Option<TokenTransaction>(new TokenTransaction(amount)) : new Option<TokenTransaction>();
        }
    }
}