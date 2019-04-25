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
        private MoneyAccount _account;
        private Money _amount;
        private DateTime _timestamp;

        public MoneyTransaction(MoneyAccount account, Money amount) : this()
        {
            _account = account;
            _amount = amount;            
        }

        private MoneyTransaction()
        {
            _timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp => _timestamp;

        public Money Amount => _amount;

        public MoneyAccount Account => _account;
    }
}