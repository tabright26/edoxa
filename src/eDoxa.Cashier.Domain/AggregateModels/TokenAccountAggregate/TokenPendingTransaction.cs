// Filename: TokenPendingTransaction.cs
// Date Created: 2019-05-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate
{
    public sealed class TokenPendingTransaction : TokenTransaction
    {
        public TokenPendingTransaction(Token amount, ActivityId linkedId) : base(amount, linkedId)
        {
        }
    }
}