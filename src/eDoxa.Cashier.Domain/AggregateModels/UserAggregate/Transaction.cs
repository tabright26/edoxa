// Filename: Transaction.cs
// Date Created: 2019-04-26
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
    public abstract class Transaction<TCurrency> : Entity<TransactionId>, ITransaction<TCurrency>
    where TCurrency : ICurrency
    {
        private ActivityId _activityId;
        private TCurrency _amount;
        private bool _pending;
        private DateTime _timestamp;

        protected Transaction(TCurrency amount, ActivityId activityId) : this()
        {
            _amount = amount;
            _activityId = activityId;
            _pending = true;
        }

        protected Transaction(TCurrency amount) : this()
        {
            _amount = amount;
            _activityId = null;
            _pending = false;
        }

        private Transaction()
        {
            _timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp => _timestamp;

        public TCurrency Amount => _amount;

        public ActivityId ActivityId => _activityId;

        public bool Pending => _pending;

        protected void Complete()
        {
            _pending = false;
        }
    }
}