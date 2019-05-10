// Filename: Transaction.cs
// Date Created: 2019-05-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public abstract class Transaction<TCurrency> : Entity<TransactionId>, ITransaction<TCurrency>
    where TCurrency : ICurrency
    {
        private TCurrency _amount;
        private TransactionDescription _description;
        private string _serviceId;
        private TransactionStatus _status;
        private DateTime _timestamp;
        private TransactionType _type;

        protected Transaction(TCurrency amount, TransactionDescription description, TransactionType type) : this()
        {
            _amount = amount;
            _description = description;
            _type = type;
            _serviceId = null;
        }

        private Transaction()
        {
            _timestamp = DateTime.UtcNow;
            _status = TransactionStatus.Pending;
        }

        public DateTime Timestamp => _timestamp;

        public TCurrency Amount => _amount;

        public TransactionDescription Description => _description;

        public TransactionType Type => _type;

        public TransactionStatus Status => _status;

        public string ServiceId => _serviceId;

        public void Pay()
        {
            _status = TransactionStatus.Paid;
        }

        public void Cancel()
        {
            _status = TransactionStatus.Canceled;
        }

        public void Fail()
        {
            _status = TransactionStatus.Failed;
        }
    }
}