// Filename: TokenTransaction.cs
// Date Created: 2019-04-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class TokenTransaction : Entity<TransactionId>, ITokenTransaction
    {
        private DateTime _timestamp;
        private TokenAccount _account;
        private Token _amount;

        public TokenTransaction(TokenAccount account, Token amount) : this()
        {
            _account = account;
            _amount = amount;            
        }

        private TokenTransaction()
        {
            _timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp => _timestamp;

        public Token Amount => _amount;

        public TokenAccount Account => _account;
    }
}