// Filename: MoneyTransaction.cs
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
    public sealed class MoneyTransaction : Entity<TransactionId>, IMoneyTransaction
    {
        private Money _money;
        private DateTime _timestamp;

        public MoneyTransaction(Money money)
        {
            _money = money;
            _timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp => _timestamp;

        public Money Amount => _money;
    }
}