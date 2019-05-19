// Filename: TokenTransaction.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.Abstractions;

namespace eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate
{
    public class TokenTransaction : Transaction<Token>, ITokenTransaction
    {
        protected TokenTransaction(Token amount, TransactionDescription description, TransactionType type) : base(amount, description, type)
        {
        }
    }
}
