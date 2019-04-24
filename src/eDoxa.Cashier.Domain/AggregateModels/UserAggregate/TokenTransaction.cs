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
        private Token _token;

        public TokenTransaction(Token token)
        {
            _token = token;
            _timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp => _timestamp;

        public Token Amount => _token;
    }
}